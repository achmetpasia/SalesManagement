using Domain.Entites.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstarctions.Repositories.BaseRepository;

public interface IRepository<T> where T : Entity
{
    DbSet<T> Table {  get; }
}
