using Application.Exceptions;
using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Profiles;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using FakeItEasy;
using System.Net;

namespace Test.Application.Orders;

public class OrderCommandHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderCommandHandlerTests()
    {
        _orderRepository = A.Fake<IOrderRepository>();
        _customerRepository = A.Fake<ICustomerRepository>();
        _productRepository = A.Fake<IProductRepository>();
        _itemRepository = A.Fake<IItemRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        var myProfile = new OrderMappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);
    }

    [Fact]
    public async Task ShouldSuccess_CreateOrder()
    {
        var command = GenerateCreateOrderCommandInstance();
        var existingCustomer = GenerateCustomer();
        var existingProduct = GenerateProduct();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.CustomerId)).Returns(existingCustomer);
        A.CallTo(() => _productRepository.FindByIdAsync(A<Guid>.Ignored)).Returns(existingProduct);

        var handler = new CreateOrderHandler(_mapper, _orderRepository, _customerRepository, _productRepository, _itemRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<CreateOrderResponse>>(result);
    }

    [Fact]
    public async Task ShouldFail_CreateOrder_WhenCustomerDoesNotExist()
    {
        var command = GenerateCreateOrderCommandInstance();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.CustomerId)).Returns(Task.FromResult<Customer>(null));

        var handler = new CreateOrderHandler(_mapper, _orderRepository, _customerRepository, _productRepository, _itemRepository, _unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldFail_CreateOrder_WhenOrderHasNoItems()
    {
        var command = GenerateCreateOrderCommandInstance();
        var existingCustomer = GenerateCustomer();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.CustomerId)).Returns(existingCustomer);

        command.Items = null;
        var handler = new CreateOrderHandler(_mapper, _orderRepository, _customerRepository, _productRepository, _itemRepository, _unitOfWork);
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldFail_CreateOrder_WhenProductDoesNotExist()
    {
        var command = GenerateCreateOrderCommandInstance();
        var existingCustomer = GenerateCustomer();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.CustomerId)).Returns(existingCustomer);
        A.CallTo(() => _productRepository.FindByIdAsync(A<Guid>.Ignored)).Returns(Task.FromResult<Product>(null));

        var handler = new CreateOrderHandler(_mapper, _orderRepository, _customerRepository, _productRepository, _itemRepository, _unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldFail_UpdateOrder_WhenItemDoesNotExist()
    {
        var command = GenerateUpdateOrderCommandInstance();

        A.CallTo(() => _itemRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Item>(null));

        var handler = new UpdateOrderHandler(_mapper, _itemRepository, _unitOfWork, _orderRepository);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldSuccess_DeleteOrder()
    {
        var command = GenerateDeleteOrderCommandInstance();
        var existingOrder = GenerateOrder();

        A.CallTo(() => _orderRepository.FindByIdAsync(command.Id)).Returns(existingOrder);

        var handler = new DeleteOrderHandler(_orderRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task ShouldFail_DeleteOrder_WhenOrderDoesNotExist()
    {
        var command = GenerateDeleteOrderCommandInstance();

        A.CallTo(() => _orderRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Order>(null));

        var handler = new DeleteOrderHandler(_orderRepository, _unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    private DeleteOrderCommand GenerateDeleteOrderCommandInstance()
    {
        return new DeleteOrderCommand(Guid.NewGuid());
    }

    private UpdateOrderCommand GenerateUpdateOrderCommandInstance()
    {
        return new UpdateOrderCommand(Guid.NewGuid(), 6);
    }

    private CreateOrderCommand GenerateCreateOrderCommandInstance()
    {
        return new CreateOrderCommand(
            Guid.NewGuid(),
            new List<CreateItemRequest> 
            {
                new CreateItemRequest(Guid.NewGuid(), 7)
            });
        
    }

    public Order GenerateOrder()
    {
        return new Order(
            DateTime.UtcNow,
            76,
            Guid.NewGuid(),
            new List<Item>
            {
                new Item(5, Guid.NewGuid(), Guid.NewGuid(), 50),
                new Item(7,  Guid.NewGuid(), Guid.NewGuid(), 26)
            });
    }

    private Customer GenerateCustomer()
    {
        return new Customer("John", "Doe", "Address", "12345");
    }

    private Product GenerateProduct()
    {
        return new Product("mango", 16.56m);
    }
}
