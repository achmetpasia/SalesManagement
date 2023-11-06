using Application.Abstarctions.Repositories.OrderRepositories;
using Domain.Entites.Orders;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;

namespace Persistence.Data.Repositories.OrderRepositories;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(Context context) : base(context)
    {
    }
}
