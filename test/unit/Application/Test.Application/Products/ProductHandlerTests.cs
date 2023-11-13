using Application.Exceptions;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Profiles;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Products;
using FakeItEasy;
using System.Linq.Expressions;

namespace Test.Application.Products;

public class ProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductHandlerTests()
    {
        _productRepository = A.Fake<IProductRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        var myProfile = new ProductMappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);
    }


    [Fact]
    public async Task ShouldSuccess_CreateProduct()
    {
        var command = GenerateCreateProductCommandInstance();

        A.CallTo(() => _productRepository.IsExistAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(false);

        var handler = new CreateProductHandler(_mapper, _productRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<CreateProductResponse>>(result);
    }

    [Fact]
    public async Task ShouldFail_CreateProduct_WhenProductAlreadyExists()
    {
        var command = GenerateCreateProductCommandInstance();

        A.CallTo(() => _productRepository.IsExistAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(true);

        var handler = new CreateProductHandler(_mapper, _productRepository, _unitOfWork);

        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldSuccess_UpdateProduct()
    {
        var command = GenerateUpdateProductCommandInstance();
        var existingProduct = GenerateAggregateInstance();

        A.CallTo(() => _productRepository.FindByIdAsync(command.Id)).Returns(existingProduct);
        A.CallTo(() => _productRepository.IsExistAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(false);

        var handler = new UpdateProductHandler(_mapper, _productRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<UpdateProductResponse>>(result);
    }

    [Fact]
    public async Task ShouldFail_UpdateProduct_WhenProductDoesNotExist()
    {
        var command = GenerateUpdateProductCommandInstance();

        A.CallTo(() => _productRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Product>(null));

        var handler = new UpdateProductHandler(_mapper, _productRepository, _unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldFail_UpdateProduct_WhenProductAlreadyExists()
    {
        var command = GenerateUpdateProductCommandInstance();
        var existingProduct = GenerateAggregateInstance();

        A.CallTo(() => _productRepository.FindByIdAsync(command.Id)).Returns(existingProduct);
        A.CallTo(() => _productRepository.IsExistAsync(A<Expression<Func<Product, bool>>>.Ignored)).Returns(true);

        var handler = new UpdateProductHandler(_mapper, _productRepository, _unitOfWork);

        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }

    private Product GenerateAggregateInstance()
    {
        return new Product("mango", 16.56m);
    }

    private UpdateProductCommand GenerateUpdateProductCommandInstance()
    {
        return new UpdateProductCommand(Guid.NewGuid(), "UpdatedProduct", 29.45m);
    }

    private CreateProductCommand GenerateCreateProductCommandInstance()
    {
        return new CreateProductCommand("Chery", 19.99m);
    }
}
