using Application.Abstarctions.Repositories.CustomerRepositories;
using Application.Abstarctions.Repositories.ItemRepositories;
using Application.Abstarctions.Repositories.OrderRepositories;
using Application.Abstarctions.Repositories.ProductRepositories;
using Application.Abstarctions;
using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Dtos;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using FakeItEasy;
using Persistence.Services.OrderService;
using System.Linq.Expressions;

namespace Test.Persistence.Orders;

public class OrderCommandServiceTests
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IItemReadRepository _itemReadRepository;
    private readonly IItemWriteRepository _itemWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly OrderCommandService _orderCommandService;

    public OrderCommandServiceTests()
    {
        _orderReadRepository = A.Fake<IOrderReadRepository>();
        _orderWriteRepository = A.Fake<IOrderWriteRepository>();
        _customerReadRepository = A.Fake<ICustomerReadRepository>();
        _productReadRepository = A.Fake<IProductReadRepository>();
        _itemReadRepository = A.Fake<IItemReadRepository>();
        _itemWriteRepository = A.Fake<IItemWriteRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();

        _orderCommandService = new OrderCommandService(
        _orderReadRepository, _orderWriteRepository, _customerReadRepository,
        _productReadRepository, _itemReadRepository, _itemWriteRepository, _unitOfWork);
    }

    [Fact]
    public async Task ShouldSuccess_CreateOrder()
    {
        var list = GenerateCreateItemRequestInstance();
        var command = GenerateCreateOrderCommandInstance(list);
        var customer = GenerateCustomerInstance();
        var product = GenerateProductInstance();

        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.CustomerId)).Returns(customer);
        A.CallTo(() => _productReadRepository.FindByIdAsync(A<Guid>._)).Returns(product);

        var result = await _orderCommandService.CreateAsync(command);

        Assert.IsType<ObjectBaseResponse<OrderDto>>(result);
        Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task ShouldFail_CreateOrder_IfCustomerNotFound()
    {
        var list = GenerateCreateItemRequestInstance();
        var command = GenerateCreateOrderCommandInstance(list);
        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.CustomerId)).Returns(Task.FromResult<Customer>(null));

        var result = await _orderCommandService.CreateAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Customer dont exist.", result.Message);
    }

    [Fact]
    public async Task ShouldFail_CreateOrder_IfProductNotFound()
    {
        var list = GenerateCreateItemRequestInstance();
        var command = GenerateCreateOrderCommandInstance(list);
        var customer = GenerateCustomerInstance();
        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.CustomerId)).Returns(customer);
        A.CallTo(() => _productReadRepository.FindByIdAsync(A<Guid>._)).Returns(Task.FromResult<Product>(null));

        var result = await _orderCommandService.CreateAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains("Product with ID", result.Message);
    }

    [Fact]
    public async Task ShouldSuccess_DeleteOrder()
    {
        var command = new DeleteOrderCommand(Guid.NewGuid());
        A.CallTo(() => _orderReadRepository.FindByIdAsync(command.Id)).Returns(GenerateOrderInstance());

        var result = await _orderCommandService.DeleteAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task ShouldFail_DeleteOrder_IfOrderNotFound()
    {
        var command = new DeleteOrderCommand(Guid.NewGuid());
        A.CallTo(() => _orderReadRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Order>(null));

        var result = await _orderCommandService.DeleteAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Order dont exist.", result.Message);
    }

    private Order GenerateOrderInstance()
    {
        var items = new List<Item>
        {
            new Item(5, Guid.NewGuid(), Guid.NewGuid(), 30),
            new Item(3, Guid.NewGuid(), Guid.NewGuid(), 35.5m),
        };

        return new Order(DateTime.UtcNow, 70, Guid.NewGuid(), items);
    }

    private List<CreateItemRequest> GenerateCreateItemRequestInstance()
    {
        var list = new List<CreateItemRequest>();

        for (int i = 0; i < 4; i++)
        {
            list.Add(new CreateItemRequest(Guid.NewGuid(), 5 + i));
        }

        return list;
    }
    

    private CreateOrderCommand GenerateCreateOrderCommandInstance(List<CreateItemRequest> createItemRequests)
    {
        return new CreateOrderCommand(Guid.NewGuid(), createItemRequests);
    }

    private UpdateOrderCommand GenerateUpdateOrderCommandInstance(Guid id, int quantity)
    {
        return new UpdateOrderCommand(id, quantity);
    }

    private Customer GenerateCustomerInstance()
    {
        return new Customer("John", "Doe", "Address", "12345");
    }

    private Product GenerateProductInstance()
    {
        return new Product("Product Name", 10.2m);
    }
}

