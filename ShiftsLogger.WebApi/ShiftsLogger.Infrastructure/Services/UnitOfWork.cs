using System.Collections.Concurrent;
using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Infrastructure.Data;
using ShiftsLogger.Infrastructure.Data.Repository;

namespace ShiftsLogger.Infrastructure.Services;

public sealed class UnitOfWork : IDisposable, IAsyncDisposable, IUnitOfWork
{
    private readonly ShiftsLoggerDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private readonly IEventPublisher _eventPublisher;
    private bool _disposed;
    
    public UnitOfWork(ShiftsLoggerDbContext context, IEventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }
    
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IDbModel
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IGenericRepository<TEntity>) _repositories[typeof(TEntity)];
        }
        
        var repository = new GenericRepository<TEntity>(_context, _eventPublisher);
        _repositories.TryAdd(typeof(TEntity), repository);
        
        return repository;
    }
    
    public async Task CompleteAsync() => await _context.SaveChangesAsync();

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            await DisposeAsyncCore();
            Dispose(false);
            GC.SuppressFinalize(this);
        }
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            await _context.DisposeAsync();
            _disposed = true;
        }
    }
}