using Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Linq.Expressions;

namespace Persistence.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Context _context;

    public ProductRepository(Context context)
    {
        _context = context;
    }

    public DbSet<Product> Table => _context.Set<Product>();

    public async Task CreateAsync(Product product)
    {
        await Table.AddAsync(product);
    }

    public void Delete(Product product)
    {
        Table.Remove(product);
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistAsync(Expression<Func<Product, bool>> predicate)
    {
        return await Table.AsNoTracking().AnyAsync(predicate);
    }

    public void Update(Product product, DateTime updatedDate)
    {
        product.SetUpdatedDate(updatedDate);

        Table.Update(product);
    }
}
