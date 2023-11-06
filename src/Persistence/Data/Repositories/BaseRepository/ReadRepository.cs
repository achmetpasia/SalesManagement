using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Core;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Linq.Expressions;

namespace Persistence.Data.Repositories.BaseRepository
{
    public class ReadRepository<T> : IReadRepository<T> where T : Entity
    {
        protected readonly Context _context;

        public ReadRepository(Context context) => _context = context;

        public DbSet<T> Table => _context.Set<T>();

        public Task<List<T>> FindAllByConditionAsync(Expression<Func<T, bool>> predicate) => Table.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<T> FindByIdAsync(Guid id) => await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate) => await Table.AsNoTracking().FirstOrDefaultAsync(predicate);

        public IQueryable<T> FindAllByCondition(Expression<Func<T, bool>> predicate) => Table.AsNoTracking().Where(predicate);
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await Table.AsNoTracking().FirstOrDefaultAsync(predicate) != null;

        public IQueryable<T> FindAllAsQueryable() => Table.AsNoTracking();

        public int Count(Expression<Func<T, bool>> predicate) => Table.AsNoTracking().Count(predicate);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) => await Table.AsNoTracking().CountAsync(predicate);

        public List<T> FindByLimitSkip(int skipCount, int limit) => Table.AsNoTracking().Skip(skipCount).Take(limit).ToList();

        public List<T> FindByLimit(int limit) => Table.AsNoTracking().Take(limit).ToList();
        public async Task<List<T>> FindByLimitAsync(int limit) => limit != default ? await Table.AsNoTracking().Take(limit).ToListAsync() : await Table.AsNoTracking().ToListAsync();

        public List<T> FindByLimit(Expression<Func<T, bool>> predicate, int limit) => Table.AsNoTracking().Take(limit).ToList();

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AsNoTracking().AnyAsync(predicate);
        }
    }
}
