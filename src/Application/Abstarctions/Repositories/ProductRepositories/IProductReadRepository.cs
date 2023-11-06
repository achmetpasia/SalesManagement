using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Products;

namespace Application.Abstarctions.Repositories.ProductRepositories;

public interface IProductReadRepository : IReadRepository<Product>
{
}
