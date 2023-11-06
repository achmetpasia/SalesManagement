using Application.Abstarctions.Repositories.ProductRepositories;
using Domain.Entites.Products;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;

namespace Persistence.Data.Repositories.ProductRepositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(Context context) : base(context)
    {
    }
}
