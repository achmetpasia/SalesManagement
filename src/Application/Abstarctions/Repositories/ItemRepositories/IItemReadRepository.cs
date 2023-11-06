using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Orders;

namespace Application.Abstarctions.Repositories.ItemRepositories;

public interface IItemReadRepository : IReadRepository<Item>
{
}
