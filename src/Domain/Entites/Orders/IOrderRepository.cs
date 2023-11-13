using System.Linq.Expressions;

namespace Domain.Entites.Orders;

public interface IOrderRepository
{
    Task CreateAsync(Order order);
    void Update(Order order, DateTime updatedDate);
    void Delete(Order order);
    Task<List<Order>> FindAllByConditionAsync(Expression<Func<Order, bool>> predicate);
    Task<List<Order>> FindAllAsync();
    Task<Order> FindByIdAsync(Guid id);
}
