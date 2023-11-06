using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Data.EntityConfigurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items").HasKey(e => e.Id);
        builder.Property(i => i.Quantity).IsRequired();

        builder.HasOne(i => i.Product).WithMany(p => p.Items).HasForeignKey(i => i.ProductId);
    }
}
