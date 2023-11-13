using Application.Features.Orders.Queries;
using Application.Features.Orders.Queries.Get;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Orders;
using FakeItEasy;

namespace Test.Application.Orders;

public class OrderQueryHandlerTests
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueryHandlerTests()
    {
        _orderRepository = A.Fake<IOrderRepository>();
    }

    [Fact]
    public async Task ShouldSuccess_GetOrders()
    {
        var entities = new List<Order>()
        {
            GenerateOrder(),
            GenerateOrder()
        };

        var command = new GetOrderQuery();

        A.CallTo(() => _orderRepository.FindAllAsync()).Returns(entities);

        var handler = new GetOrderHandler(_orderRepository);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<ArrayBaseResponse<OrderResponseDto>>(result);
    }

    private Order GenerateOrder()
    {
        return new Order(
            DateTime.UtcNow,
            56,
            Guid.NewGuid(),
            new List<Item>
            {
                new Item(5, Guid.NewGuid(), Guid.NewGuid(), 50),
                new Item(7,  Guid.NewGuid(), Guid.NewGuid(), 26)
            });
    }
}
