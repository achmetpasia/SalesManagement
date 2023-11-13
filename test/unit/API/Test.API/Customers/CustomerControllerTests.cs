using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesManagement.API.Controllers;
using SalesManagement.API.Models.Customers;

namespace Test.API.Customers;

public class CustomerControllerTests
{
    [Fact]
    public async Task Create_Customer_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new CustomerController(mediatorMock.Object);

        var id = Guid.NewGuid();
        var createCommand = new CreateCustomerCommand(id, "John", "Doe", "123 Main St", "12345");

        var expectedResult = new ObjectBaseResponse<CreateCustomerResponse>
        {
            StatusCode = System.Net.HttpStatusCode.Created,
            Data = new CreateCustomerResponse { }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await controller.Create(createCommand);

        Assert.IsType<ObjectResult>(result);

        var createdResult = Assert.IsType<ObjectBaseResponse<CreateCustomerResponse>>(((ObjectResult)result).Value);
        Assert.Equal(expectedResult, createdResult);
        Assert.Equal((int)expectedResult.StatusCode, ((ObjectResult)result).StatusCode);
    }

    [Fact]
    public async Task Update_Customer_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new CustomerController(mediatorMock.Object);

        var customerId = Guid.NewGuid();
        var updateRequest = new UpdateCustomerRequest
        {
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            Address = "UpdatedAddress",
            PostalCode = "UpdatedPostalCode"
        };

        var expectedResult = new ObjectBaseResponse<UpdateCustomerResponse>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Data = new UpdateCustomerResponse { Id = customerId }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await controller.Update(customerId, updateRequest);

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        var response = Assert.IsType<ObjectBaseResponse<UpdateCustomerResponse>>(statusCodeResult.Value);
        Assert.Equal(expectedResult, response);
        Assert.Equal((int)expectedResult.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task Delete_Customer_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new CustomerController(mediatorMock.Object);

        var customerId = Guid.NewGuid();

        var result = await controller.Delete(customerId);

        Assert.IsType<NoContentResult>(result);
        mediatorMock.Verify(x => x.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
