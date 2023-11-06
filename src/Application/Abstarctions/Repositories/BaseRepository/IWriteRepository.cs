using Domain.Entites.Core;

namespace Application.Abstarctions.Repositories.BaseRepository;

public interface IWriteRepository<T> : IRepository<T> where T : Entity
{
    void Create(T entity);
    Task CreateAsync(T entity);
    Task CreateMany(List<T> entities);
    void Update(T entity, DateTime updatedDate);
    void UpdateMany(List<T> entities, DateTime updatedDate);
    void HardDelete(T entity);
    void HardDeleteMany(List<T> entity);
}
