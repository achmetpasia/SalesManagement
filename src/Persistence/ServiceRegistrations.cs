using Domain.Entites.Core;
using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories;

namespace Persistence;

public static class ServiceRegistrations
{
    public static void AddPersistenceServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
        service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        service.AddScoped<ICustomerRepository, CustomerRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IItemRepository, ItemRepository>();
    }
}
