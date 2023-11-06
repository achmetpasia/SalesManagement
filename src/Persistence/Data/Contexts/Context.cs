using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data.Contexts;

public class Context : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Product> Products { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public void DetachedAll()
    {
        ChangeTracker.Entries().Where(e => e.Entity != null).ToList()
            .ForEach(e => e.State = EntityState.Detached);
    }
}
