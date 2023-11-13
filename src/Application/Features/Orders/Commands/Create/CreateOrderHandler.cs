using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using MediatR;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ObjectBaseResponse<CreateOrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IMapper mapper, IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IItemRepository itemRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id ?? Guid.NewGuid();

        var customer = await _customerRepository.FindByIdAsync(request.CustomerId);
        if (customer == null) throw new NotFoundException($"This customer with customerId:{request.CustomerId} dont exist.");

        if (request.Items == null) throw new ConflictException("Order should have at least one Item.");

        decimal totalPrice = 0;

        var itemList = new List<Item>();

        foreach (var itemRequest in request.Items)
        {
            var product = await _productRepository.FindByIdAsync(itemRequest.ProductId);
            if (product == null) throw new NotFoundException("This product dont exist.");

            decimal itemPrice = product.Price * itemRequest.Quantity;
            totalPrice += itemPrice;

            var item = new Item(itemRequest.Quantity, product.Id, id, itemPrice);

            itemList.Add(item);
        }

        var order = new Order(DateTime.UtcNow, totalPrice, request.CustomerId, itemList) { Id = id }; 

        await _orderRepository.CreateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<ObjectBaseResponse<CreateOrderResponse>>(order);

        return response;
    }
}
