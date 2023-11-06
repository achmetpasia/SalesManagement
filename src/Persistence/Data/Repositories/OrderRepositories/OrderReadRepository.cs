using Application.Abstarctions.Repositories.OrderRepositories;
using Domain.Entites.Orders;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;

namespace Persistence.Data.Repositories.OrderRepositories;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(Context context) : base(context)
    {
    }
}
