using System.Data;

namespace Domain.Entites.Core;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task TransactionCommitAsync();
    Task RollbackTransactionAsync();
    bool HasTransaction { get; }
    IDbTransaction Transaction { get; }
}
