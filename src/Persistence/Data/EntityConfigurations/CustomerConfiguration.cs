using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Data.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers").HasKey(e => e.Id);
        builder.Property(c => c.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
        builder.Property(c => c.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50);
        builder.Property(c => c.Address).HasColumnName("Address").IsRequired().HasMaxLength(100);
        builder.Property(c => c.PostalCode).HasColumnName("PostCode").IsRequired().HasMaxLength(20);

        builder.HasMany(c => c.Orders).WithOne(o => o.Customer).HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
