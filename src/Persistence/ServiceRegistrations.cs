using Application.Abstarctions;
using Application.Abstarctions.Repositories.CustomerRepositories;
using Application.Abstarctions.Repositories.ItemRepositories;
using Application.Abstarctions.Repositories.OrderRepositories;
using Application.Abstarctions.Repositories.ProductRepositories;
using Application.Features.Customers.Services;
using Application.Features.Orders.Services;
using Application.Features.Products.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.CustomerRepositories;
using Persistence.Data.Repositories.ItemRepository;
using Persistence.Data.Repositories.OrderRepositories;
using Persistence.Data.Repositories.ProductRepositories;
using Persistence.Services.CustomerService;
using Persistence.Services.OrderService;
using Persistence.Services.ProductServices;

namespace Persistence;

public static class ServiceRegistrations
{
    public static void AddPersistenceServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
        service.AddScoped<ICustomerCommandService, CustomerCommandService>();
        service.AddScoped<IProductCommandService, ProductCommandService>();
        service.AddScoped<IOrderCommandService, OrderCommandService>();
        service.AddScoped<IOrderQueryService, OrderQueryService>();
        service.AddScoped<IItemReadRepository, ItemReadRepository>();
        service.AddScoped<IItemWriteRepository, ItemWriteRepository>();

        service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        service.AddScoped<IOrderReadRepository, OrderReadRepository>();
        service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        service.AddScoped<IProductReadRepository, ProductReadRepository>();
        service.AddScoped<IProductWriteRepository, ProductWriteRepository>();

    }
}
