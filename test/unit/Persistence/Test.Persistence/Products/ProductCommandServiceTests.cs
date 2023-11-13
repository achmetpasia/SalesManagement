using Application.Abstarctions.Repositories.ProductRepositories;
using Application.Abstarctions;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Products;
using FakeItEasy;
using Persistence.Services.ProductServices;
using System.Linq.Expressions;
using Domain.Entites.Core;

namespace Test.Persistence.Products;

public class ProductCommandServiceTests
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductCommandService _productCommandService;

    public ProductCommandServiceTests()
    {
        _productWriteRepository = A.Fake<IProductWriteRepository>();
        _productReadRepository = A.Fake<IProductReadRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _productCommandService = new ProductCommandService(_productWriteRepository, _productReadRepository, _unitOfWork);
    }

    [Fact]
    public async Task ShouldSuccess_CreateProduct()
    {
        var command = GenerateCreateProductCommandInstance();

        A.CallTo(() => _productReadRepository.IsExistsAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(false);

        var result = await _productCommandService.CreateAsync(command);

        Assert.IsType<ObjectBaseResponse<ProductDto>>(result);
        Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task ShouldFail_CreateProduct_IfProductExists()
    {
        var command = GenerateCreateProductCommandInstance();

        A.CallTo(() => _productReadRepository.IsExistsAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(true);

        var result = await _productCommandService.CreateAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.Conflict, result.StatusCode);
        Assert.Equal("Already exist.", result.Message);
    }

    [Fact]
    public async Task ShouldSuccess_UpdateProduct()
    {
        var existingProduct = GenerateProductInstance();
        var command = GenerateUpdateProductCommandInstance(existingProduct.Id);


        A.CallTo(() => _productReadRepository.FindByIdAsync(command.Id)).Returns(existingProduct);
        A.CallTo(() => _productReadRepository.IsExistsAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(false);

        var result = await _productCommandService.UpdateAsync(command);

        Assert.IsType<ObjectBaseResponse<ProductDto>>(result);
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(command.Id, result.Data.Id); 
    }

    [Fact]
    public async Task ShouldFail_UpdateProduct_IfProductNotFound()
    {
        var existingProduct = GenerateProductInstance();
        var command = GenerateUpdateProductCommandInstance(existingProduct.Id);

        A.CallTo(() => _productReadRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Product>(null));

        var result = await _productCommandService.UpdateAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Product dont exist.", result.Message);
    }

    [Fact]
    public async Task ShouldSuccess_DeleteProduct()
    {
        var command = new DeleteProductCommand(Guid.NewGuid());

        var existingProduct = GenerateProductInstance();
        A.CallTo(() => _productReadRepository.FindByIdAsync(command.Id)).Returns(existingProduct);

        var result = await _productCommandService.DeleteAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task ShouldFail_DeleteProduct_IfProductNotFound()
    {
        var command = new DeleteProductCommand(Guid.NewGuid());

        A.CallTo(() => _productReadRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Product>(null));

        var result = await _productCommandService.DeleteAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Product dont exist.", result.Message);
    }

    private Product GenerateProductInstance()
    {
        return new Product("Product Name", 10.0m);
    }

    private static CreateProductCommand GenerateCreateProductCommandInstance()
    {
        return new CreateProductCommand("Product Name", 10.0m);
    }

    private static UpdateProductCommand GenerateUpdateProductCommandInstance(Guid id)
    {
        return new UpdateProductCommand(id, "Updated Product Name", 20.0m);
    }
}
