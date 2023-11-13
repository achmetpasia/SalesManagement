using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Linq.Expressions;

namespace Persistence.Data.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly Context _context;

    public ItemRepository(Context context)
    {
        _context = context;
    }

    public DbSet<Item> Table => _context.Set<Item>();

    public void Delete(Item item)
    {
        Table.Remove(item);
    }

    public async Task<Item> FindByIdAsync(Guid id)
    {
        return await Table.AsNoTracking().Include(s => s.Order).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistsAsync(Expression<Func<Item, bool>> predicate)
    {
        return await Table.AsNoTracking().AnyAsync(predicate);
    }

    public void Update(Item item, DateTime updatedDate)
    {
        item.SetUpdatedDate(updatedDate);

        Table.Update(item);
    }
}
