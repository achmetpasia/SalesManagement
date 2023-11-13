using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesManagement.API.Controllers;
using SalesManagement.API.Models.Orders;

namespace Test.API.Orders;

public class OrderControllerTests
{
    [Fact]
    public async Task Create_Order_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new OrderController(mediatorMock.Object);

        var orderId = Guid.NewGuid();
        var createRequest = new CreateOrderRequest
        {
            CustomerId = Guid.NewGuid(),
            Items = new List<CreateItemRequest>
        {
            new CreateItemRequest(Guid.NewGuid(), 4)
        }
        };

        var expectedResult = new ObjectBaseResponse<CreateOrderResponse>
        {
            StatusCode = System.Net.HttpStatusCode.Created,
            Data = new CreateOrderResponse { Id = orderId }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await controller.Create(createRequest);

        Assert.IsType<ObjectResult>(result);

        var response = Assert.IsType<ObjectBaseResponse<CreateOrderResponse>>(((ObjectResult)result).Value);
        Assert.Equal(expectedResult, response);
        Assert.Equal((int)expectedResult.StatusCode, ((ObjectResult)result).StatusCode);
    }


    [Fact]
    public async Task Update_Order_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new OrderController(mediatorMock.Object);

        var orderId = Guid.NewGuid();
        var updateRequest = new UpdateOrderRequest
        {
            Id = orderId,
            Quantity = 10
        };

        var expectedResult = new ObjectBaseResponse<UpdateOrderResponse>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Data = new UpdateOrderResponse { Id = orderId }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<UpdateOrderCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await controller.Update(updateRequest);

        Assert.IsType<ObjectResult>(result);

        var response = Assert.IsType<ObjectBaseResponse<UpdateOrderResponse>>(((ObjectResult)result).Value);
        Assert.Equal(expectedResult, response);
        Assert.Equal((int)expectedResult.StatusCode, ((ObjectResult)result).StatusCode);
    }


    [Fact]
    public async Task Delete_Order_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new OrderController(mediatorMock.Object);

        var orderId = Guid.NewGuid();

        mediatorMock.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResponseBase { StatusCode = System.Net.HttpStatusCode.NoContent });

        var result = await controller.Delete(orderId);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal((int)System.Net.HttpStatusCode.NoContent, noContentResult.StatusCode);
    }
}
