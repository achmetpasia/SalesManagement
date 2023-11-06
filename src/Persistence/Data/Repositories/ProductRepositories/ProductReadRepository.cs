using Application.Abstarctions.Repositories.ProductRepositories;
using Domain.Entites.Products;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;

namespace Persistence.Data.Repositories.ProductRepositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(Context context) : base(context)
    {
    }
}
