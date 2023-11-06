using Application.Abstarctions.Repositories.ItemRepositories;
using Domain.Entites.Orders;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;

namespace Persistence.Data.Repositories.ItemRepository;

public class ItemReadRepository : ReadRepository<Item>, IItemReadRepository
{
    public ItemReadRepository(Context context) : base(context)
    {
    }
}
