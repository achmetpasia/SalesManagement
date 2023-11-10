using Application.Abstarctions;
using Application.Abstarctions.Repositories.CustomerRepositories;
using Application.Abstarctions.Repositories.ItemRepositories;
using Application.Abstarctions.Repositories.OrderRepositories;
using Application.Abstarctions.Repositories.ProductRepositories;
using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Services;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services.OrderService;

public class OrderCommandService : IOrderCommandService
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IItemReadRepository _itemReadRepository;
    private readonly IItemWriteRepository _itemWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCommandService(
        IOrderReadRepository orderReadRepository,
        IOrderWriteRepository orderWriteRepository,
        ICustomerReadRepository customerReadRepository,
        IProductReadRepository productReadRepository,
        IItemReadRepository itemReadRepository,
        IItemWriteRepository itemWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _orderReadRepository = orderReadRepository;
        _orderWriteRepository = orderWriteRepository;
        _customerReadRepository = customerReadRepository;
        _productReadRepository = productReadRepository;
        _itemReadRepository = itemReadRepository;
        _itemWriteRepository = itemWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<OrderDto>> CreateAsync(CreateOrderCommand command)
    {
        var id = command.Id ?? Guid.NewGuid();

        var customer = await _customerReadRepository.FindByIdAsync(command.CustomerId);
        if (customer == null) return new ObjectBaseResponse<OrderDto>(System.Net.HttpStatusCode.NotFound, "Customer dont exist.");

        decimal totalPrice = 0;

        var itemList = new List<Item>();

        foreach (var itemRequest in command.Items)
        {
            var product = await _productReadRepository.FindByIdAsync(itemRequest.ProductId);
            if (product == null)
            {
                return new ObjectBaseResponse<OrderDto>(System.Net.HttpStatusCode.NotFound, $"Product with ID {itemRequest.ProductId} not found.");
            }

            var isExist = await _itemReadRepository.IsExistsAsync(s => s.OrderId == id && s.ProductId == product.Id);
            if (isExist) return new ObjectBaseResponse<OrderDto>(System.Net.HttpStatusCode.Conflict, $"Item already exist.");

            decimal itemPrice = product.Price * itemRequest.Quantity;

            totalPrice += itemPrice;
            
            var item = new Item(itemRequest.Quantity, product.Id, id, itemPrice);

            itemList.Add(item);
        }

        var order = new Order(DateTime.UtcNow, totalPrice, command.CustomerId, itemList);

        await _orderWriteRepository.CreateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return new ObjectBaseResponse<OrderDto>(new OrderDto(order.Id), System.Net.HttpStatusCode.Created);
    }

    public async Task<ResponseBase> DeleteAsync(DeleteOrderCommand command)
    {
        var entity = await _orderReadRepository.FindByIdAsync(command.Id);
        if (entity == null) return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Order dont exist." };

        _orderWriteRepository.HardDelete(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NoContent };
    }

    public async Task<ObjectBaseResponse<OrderDto>> UpdateAsync(UpdateOrderCommand command)
    {
        var existingItem = _itemReadRepository.FindAllByCondition(s => s.Id == command.Id).Include(s => s.Order).FirstOrDefault();
        if (existingItem == null)
        {
            return new ObjectBaseResponse<OrderDto>(System.Net.HttpStatusCode.NotFound, $"Item dont exist");
        }

        var order = existingItem.Order;

        var totalPrice = order.TotalPrice;

        var oldItemPrice = existingItem.ItemPrice;

        var orderDate = DateTime.UtcNow;

        decimal newTotalPrice = 0;

        if (command.Quantity > 0)
        {
            var productPrice = existingItem.ItemPrice / existingItem.Quantity;

            existingItem.SetQuantity(command.Quantity);

            var newItemPrice = productPrice * command.Quantity;

            existingItem.SetItemPrice(newItemPrice);

            newTotalPrice = totalPrice - oldItemPrice + newItemPrice;

            _itemWriteRepository.Update(existingItem, orderDate);

        }
        else
        {
            newTotalPrice = totalPrice - oldItemPrice;

            existingItem.SetQuantity(command.Quantity);

            existingItem.SetItemPrice(0);

            _itemWriteRepository.HardDelete(existingItem);
        }

        order.SetTotalPrice(newTotalPrice);

        order.SetOrderDate(orderDate);

        _orderWriteRepository.Update(order, orderDate);
        await _unitOfWork.SaveChangesAsync();

        return new ObjectBaseResponse<OrderDto>(new OrderDto(order.Id), System.Net.HttpStatusCode.OK);
    }
}
