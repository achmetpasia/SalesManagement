using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesManagement.API.Controllers;
using SalesManagement.API.Models.Products;

namespace Test.API.Products;

public class ProductControllerTests
{
    [Fact]
    public async Task Create_Product_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new ProductController(mediatorMock.Object);

        var productId = Guid.NewGuid();
        var createCommand = new CreateProductCommand("Sample Product", 19.99m);

        var expectedResult = new ObjectBaseResponse<CreateProductResponse>
        {
            StatusCode = System.Net.HttpStatusCode.Created,
            Data = new CreateProductResponse { Id = productId }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await controller.Create(createCommand);

        var createdResult = Assert.IsType<ObjectResult>(result); 
        var response = Assert.IsType<ObjectBaseResponse<CreateProductResponse>>(createdResult.Value);
        Assert.Equal(expectedResult, response);
        Assert.Equal((int)expectedResult.StatusCode, createdResult.StatusCode);
    }


    [Fact]
    public async Task Update_Product_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new ProductController(mediatorMock.Object);

        var productId = Guid.NewGuid();
        var updateRequest = new UpdateProductRequest { Name = "Updated Product Name", Price = 29.99m };

        var expectedResult = new ObjectBaseResponse<UpdateProductResponse>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Data = new UpdateProductResponse { Id = productId }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult)
            .Verifiable();

        var result = await controller.Update(productId, updateRequest);

        var objectResult = Assert.IsType<ObjectResult>(result);
        var response = Assert.IsType<ObjectBaseResponse<UpdateProductResponse>>(objectResult.Value);
        
        Assert.Equal(expectedResult, response);
        Assert.Equal((int)expectedResult.StatusCode, objectResult.StatusCode);
    }


    [Fact]
    public async Task Delete_Product_ShouldSuccess()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new ProductController(mediatorMock.Object);

        var productId = Guid.NewGuid();

        var result = await controller.Delete(productId);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal((int)System.Net.HttpStatusCode.NoContent, noContentResult.StatusCode);
    }
}
