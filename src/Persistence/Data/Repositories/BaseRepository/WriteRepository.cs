using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Core;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace Persistence.Data.Repositories.BaseRepository;

public class WriteRepository<T> : IWriteRepository<T> where T : Entity
{
    protected readonly Context _context;

    public WriteRepository(Context context) => _context = context;

    public DbSet<T> Table => _context.Set<T>();

    public void Create(T entity)
    {
        Table.Add(entity);
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task CreateMany(List<T> entities)
    {
        await _context.AddRangeAsync(entities);
    }

    public void HardDelete(T entity) => Table.Remove(entity);

    public void HardDeleteMany(List<T> entity) => Table.RemoveRange(entity);

    public void Update(T entity, DateTime UpdatedDate)
    {
        entity.SetUpdatedDate(UpdatedDate);

        Table.Update(entity);
    }

    public void UpdateMany(List<T> entities, DateTime updatedDate)
    {
        entities.ForEach(e => Update(e, updatedDate));

        Table.UpdateRange(entities);
    }
}
