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
        
        InitializeDatabase();
    }
    
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IDbModel
    {
        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(typeof(TEntity), CreateRepository);
        
        object CreateRepository(Type type) => 
            new GenericRepository<TEntity>(_context, _eventPublisher);
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

    private void InitializeDatabase()
    {
        if (!_context.Database.CanConnect())
        {
            _context.Database.EnsureCreated();
        }
    }
}