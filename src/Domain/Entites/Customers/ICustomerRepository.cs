using System.Linq.Expressions;

namespace Domain.Entites.Customers;

public interface ICustomerRepository 
{
    Task CreateAsync(Customer customer);
    void Update(Customer customer, DateTime updatedDate);
    void Delete(Customer customer);
    Task<Customer> FindByIdAsync(Guid id);
    Task<bool> IsExistAsync(Expression<Func<Customer, bool>> predicate);
}
