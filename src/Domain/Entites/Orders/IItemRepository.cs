using Domain.Entites.Customers;
using System.Linq.Expressions;

namespace Domain.Entites.Orders;

public interface IItemRepository
{
    void Update(Item item, DateTime updatedDate);
    void Delete(Item item);
    Task<Item> FindByIdAsync(Guid id);
    Task<bool> IsExistsAsync(Expression<Func<Item, bool>> predicate);
}
