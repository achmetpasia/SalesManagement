using Domain.Entites.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Linq.Expressions;

namespace Persistence.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly Context _context;

    public CustomerRepository(Context context)
    {
        _context = context;
    }

    public DbSet<Customer> Table => _context.Set<Customer>();

    public async Task CreateAsync(Customer customer)
    {
        await Table.AddAsync(customer);
    }

    public void Delete(Customer customer)
    {
        Table.Remove(customer);
    }

    public async Task<Customer> FindByIdAsync(Guid id)
    {
        return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistAsync(Expression<Func<Customer, bool>> predicate)
    {
        return await Table.AsNoTracking().AnyAsync(predicate);
    }

    public void Update(Customer customer, DateTime updatedDate)
    {
        customer.SetUpdatedDate(updatedDate);

        Table.Update(customer);
    }
}
