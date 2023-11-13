using Domain.Entites.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories;
using System.Linq.Expressions;
using Xunit;

namespace Integration.Persistence;

public class CustomerRepositoryTests :  IDisposable
{
    private readonly DbContextOptions<Context> _options;


    public CustomerRepositoryTests()
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
        var repository = new CustomerRepository(context);

        var customer = CreateSampleCustomer();

        await repository.CreateAsync(customer);
        await context.SaveChangesAsync();

        var createdCustomer = await repository.FindByIdAsync(customer.Id);
        Assert.NotNull(createdCustomer);
        Assert.Equal(customer.Id, createdCustomer.Id);
    }

    [Fact]
    public async Task ShouldSuccess_Update()
    {
        using var context = new Context(_options);
        var repository = new CustomerRepository(context);

        var customer = CreateSampleCustomer();

        await repository.CreateAsync(customer);
        await context.SaveChangesAsync();

        customer.SetFirstName("UpdatedFirstName");
        customer.SetLastName("UpdatedLastName");

        var updatedDateBeforeUpdate = customer.UpdatedDate;

        repository.Update(customer, DateTime.Now);
        await context.SaveChangesAsync();

        var updatedCustomer = await repository.FindByIdAsync(customer.Id);
        Assert.NotNull(updatedCustomer);
        Assert.Equal(customer.FirstName, updatedCustomer.FirstName);
        Assert.Equal(customer.LastName, updatedCustomer.LastName);
        Assert.NotEqual(updatedDateBeforeUpdate, updatedCustomer.UpdatedDate);
    }

    [Fact]
    public async Task ShouldSuccess_Delete()
    {
        using var context = new Context(_options);
        var repository = new CustomerRepository(context);

        var customer = CreateSampleCustomer();

        await repository.CreateAsync(customer);
        await context.SaveChangesAsync();

        repository.Delete(customer);
        await context.SaveChangesAsync();

        var deletedCustomer = await repository.FindByIdAsync(customer.Id);
        Assert.Null(deletedCustomer);
    }

    [Fact]
    public async Task ShouldSuccess_FindByIdAsync()
    {
        using var context = new Context(_options);
        var repository = new CustomerRepository(context);

        var customer = CreateSampleCustomer();

        await repository.CreateAsync(customer);
        await context.SaveChangesAsync();

        var foundCustomer = await repository.FindByIdAsync(customer.Id);
        Assert.NotNull(foundCustomer);
        Assert.Equal(customer.Id, foundCustomer.Id);
    }

    [Fact]
    public async Task ShouldSuccess_IsExistsAsync()
    {
        using var context = new Context(_options);
        var repository = new CustomerRepository(context);

        var customer = CreateSampleCustomer();

        await repository.CreateAsync(customer);
        await context.SaveChangesAsync();

        Expression<Func<Customer, bool>> predicate = x => x.Id == customer.Id;
        var doesExist = await repository.IsExistAsync(predicate);
        Assert.True(doesExist);
    }

    private Customer CreateSampleCustomer()
    {
        return new Customer("John", "Doe", "123 Main St", "12345");
    }
}
