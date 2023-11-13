using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories;
using Xunit;

namespace Integration.Persistence;

public class OrderRepositoryTests : IDisposable
{
    private readonly DbContextOptions<Context> _options;

    public OrderRepositoryTests()
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
    public async Task ShouldSuccess_Create()
    {
        using var context = new Context(_options);
        var repository = new OrderRepository(context);

        var order = CreateSampleOrder();

        await repository.CreateAsync(order);
        await context.SaveChangesAsync();

        var createdOrder = await repository.FindByIdAsync(order.Id);
        Assert.NotNull(createdOrder);
        Assert.Equal(order.Id, createdOrder.Id);
    }

    [Fact]
    public async Task ShouldSuccess_Update()
    {
        using var context = new Context(_options);
        var repository = new OrderRepository(context);

        var order = CreateSampleOrder();

        await repository.CreateAsync(order);
        await context.SaveChangesAsync();

        order.SetTotalPrice(99.99m);

        var updatedDateBeforeUpdate = order.UpdatedDate;

        repository.Update(order, DateTime.Now);
        await context.SaveChangesAsync();

        var updatedOrder = await repository.FindByIdAsync(order.Id);
        Assert.NotNull(updatedOrder);
        Assert.Equal(99.99m, updatedOrder.TotalPrice);
        Assert.NotEqual(updatedDateBeforeUpdate, updatedOrder.UpdatedDate);
    }

    [Fact]
    public async Task ShouldSuccess_Delete()
    {
        using var context = new Context(_options);
        var repository = new OrderRepository(context);

        var order = CreateSampleOrder();

        await repository.CreateAsync(order);
        await context.SaveChangesAsync();

        repository.Delete(order);
        await context.SaveChangesAsync();

        var deletedOrder = await repository.FindByIdAsync(order.Id);
        Assert.Null(deletedOrder);
    }

    [Fact]
    public async Task ShouldSuccess_FindById()
    {
        using var context = new Context(_options);
        var repository = new OrderRepository(context);

        var order = CreateSampleOrder();

        await repository.CreateAsync(order);
        await context.SaveChangesAsync();

        var foundOrder = await repository.FindByIdAsync(order.Id);
        Assert.NotNull(foundOrder);
        Assert.Equal(order.Id, foundOrder.Id);
    }

    private Order CreateSampleOrder()
    {
        var customer = new Customer("John", "Doe", "123 Main St", "12345");
        var product1 = new Product("Product 1", 10.0m);
        var product2 = new Product("Product 2", 20.0m);

        var item1 = new Item(2, product1.Id, Guid.NewGuid(), 0);
        var item2 = new Item(3, product2.Id, Guid.NewGuid(), 0);

        item1.SetItemPrice(item1.Quantity * product1.Price);
        item2.SetItemPrice(item2.Quantity * product2.Price);

        var order = new Order(DateTime.UtcNow, 0, customer.Id, new List<Item> { item1, item2 });

        item1.SetOrderId(order.Id);
        item2.SetOrderId(order.Id);

        order.SetTotalPrice(item1.ItemPrice + item2.ItemPrice);

        return order;
    }
}
