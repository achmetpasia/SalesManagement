using System.Linq.Expressions;

namespace Domain.Entites.Products;

public interface IProductRepository
{
    Task CreateAsync(Product product);
    void Update(Product product, DateTime updatedDate);
    void Delete(Product product);
    Task<Product> FindByIdAsync(Guid id);
    Task<bool> IsExistAsync(Expression<Func<Product, bool>> predicate);
}
