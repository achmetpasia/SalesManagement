using Application.Abstarctions;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Data.Contexts;
using System.Data;

namespace Persistence.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            var result = await _context.SaveChangesAsync();

            _context.DetachedAll();

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public async Task BeginTransactionAsync()
    {
        try
        {
            if (_context.Database.CurrentTransaction is null) _transaction ??= await _context.Database.BeginTransactionAsync();

            else _transaction ??= _context.Database.CurrentTransaction;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public async Task TransactionCommitAsync()
    {
        try
        {
            await SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public bool HasTransaction => _transaction != null;

    public IDbTransaction Transaction => HasTransaction ? _transaction.GetDbTransaction() : null;
}
