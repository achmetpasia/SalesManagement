using Domain.Entites.Customers;
using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Linq.Expressions;

namespace Persistence.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context)
    {
        _context = context;
    }

    public DbSet<Order> Table => _context.Set<Order>();

    public async Task CreateAsync(Order order)
    {
        await Table.AddAsync(order);
    }

    public void Delete(Order order)
    {
        Table.Remove(order);
    }

    public async Task<List<Order>> FindAllAsync()
    {
        return await Table.AsNoTracking().Include(s => s.Items).ToListAsync();
    }

    public async Task<List<Order>> FindAllByConditionAsync(Expression<Func<Order, bool>> predicate)
    {
        return await Table.AsNoTracking().Where(predicate).Include(s => s.Items).ToListAsync();
    }

    public async Task<Order> FindByIdAsync(Guid id)
    {
        return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Order order, DateTime updatedDate)
    {
        order.SetUpdatedDate(updatedDate);

        Table.Update(order);
    }
}
