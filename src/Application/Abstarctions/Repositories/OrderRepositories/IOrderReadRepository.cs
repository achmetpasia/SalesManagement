using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Orders;

namespace Application.Abstarctions.Repositories.OrderRepositories;

public interface IOrderReadRepository : IReadRepository<Order>
{
}
