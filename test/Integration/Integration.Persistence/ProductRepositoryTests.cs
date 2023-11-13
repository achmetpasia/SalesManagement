using Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories;
using System.Linq.Expressions;
using Xunit;

namespace Integration.Persistence;

public class ProductRepositoryTests : IDisposable
{
    private readonly DbContextOptions<Context> _options;

    public ProductRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    public void Dispose()
    {
        using var context = new Context(_options);
        context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task ShouldSuccess_CreateProduct()
    {
        using var context = new Context(_options);
        var repository = new ProductRepository(context);
        var product = CreateSampleProduct();

        await repository.CreateAsync(product);
        await context.SaveChangesAsync();

        var createdProduct = await repository.FindByIdAsync(product.Id);
        Assert.NotNull(createdProduct);
        Assert.Equal(product.Id, createdProduct.Id);
        Assert.Equal(product.Name, createdProduct.Name);
        Assert.Equal(product.Price, createdProduct.Price);
    }

    [Fact]
    public async Task ShouldSuccess_UpdateProduct()
    {
        using var context = new Context(_options);
        var repository = new ProductRepository(context);
        var product = CreateSampleProduct();

        await repository.CreateAsync(product);
        await context.SaveChangesAsync();

        product.SetName("Updated Product");
        product.SetPrice(29.99m);
        repository.Update(product, DateTime.Now);
        await context.SaveChangesAsync();

        var updatedProduct = await repository.FindByIdAsync(product.Id);
        Assert.NotNull(updatedProduct);
        Assert.Equal("Updated Product", updatedProduct.Name);
        Assert.Equal(29.99m, updatedProduct.Price);
    }

    [Fact]
    public async Task ShouldSuccess_DeleteProduct()
    {
        // Arrange
        using var context = new Context(_options);
        var repository = new ProductRepository(context);
        var product = CreateSampleProduct();

        await repository.CreateAsync(product);
        await context.SaveChangesAsync();

        // Act
        repository.Delete(product);
        await context.SaveChangesAsync();

        // Assert
        var deletedProduct = await repository.FindByIdAsync(product.Id);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task ShouldSuccess_FindProductById()
    {
        // Arrange
        using var context = new Context(_options);
        var repository = new ProductRepository(context);
        var product = CreateSampleProduct();

        await repository.CreateAsync(product);
        await context.SaveChangesAsync();

        // Act
        var foundProduct = await repository.FindByIdAsync(product.Id);

        // Assert
        Assert.NotNull(foundProduct);
        Assert.Equal(product.Id, foundProduct.Id);
        Assert.Equal(product.Name, foundProduct.Name);
        Assert.Equal(product.Price, foundProduct.Price);
    }

    [Fact]
    public async Task ShouldSuccess_IsProductExist()
    {
        // Arrange
        using var context = new Context(_options);
        var repository = new ProductRepository(context);
        var product = CreateSampleProduct();

        await repository.CreateAsync(product);
        await context.SaveChangesAsync();

        // Act
        var doesExist = await repository.IsExistAsync(x => x.Id == product.Id);

        // Assert
        Assert.True(doesExist);
    }

    private Product CreateSampleProduct()
    {
        return new Product("Sample Product", 19.99m);
    }
}
