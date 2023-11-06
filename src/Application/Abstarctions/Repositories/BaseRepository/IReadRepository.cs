using Domain.Entites.Core;
using System.Linq.Expressions;

namespace Application.Abstarctions.Repositories.BaseRepository;

public interface IReadRepository<T> : IRepository<T> where T : Entity
{
    IQueryable<T> FindAllAsQueryable();
    IQueryable<T> FindAllByCondition(Expression<Func<T, bool>> predicate);
    Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> FindByIdAsync(Guid id);
    Task<List<T>> FindAllByConditionAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    int Count(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    List<T> FindByLimitSkip(int skipCount, int limit);
    List<T> FindByLimit(int limit);
    Task<List<T>> FindByLimitAsync(int limit);
    Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate);
    List<T> FindByLimit(Expression<Func<T, bool>> predicate, int limit);
}
