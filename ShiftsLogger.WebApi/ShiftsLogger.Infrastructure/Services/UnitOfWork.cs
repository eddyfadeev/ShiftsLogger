using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Infrastructure.Data;
using ShiftsLogger.Infrastructure.Data.Repository;

namespace ShiftsLogger.Infrastructure.Services;

public sealed class UnitOfWork<TEntity> : IDisposable, IAsyncDisposable, IUnitOfWork<TEntity>
    where TEntity : class, IDbModel
{
    private readonly ShiftsLoggerDbContext _context;
    private readonly Lazy<IGenericRepository<TEntity>> _repository;
    private bool _disposed = false;

    public IGenericRepository<TEntity> Repository =>
        _repository.Value;
    
    public UnitOfWork(ShiftsLoggerDbContext context, IEventPublisher eventPublisher)
    {
        _context = context;
        _repository = new Lazy<IGenericRepository<TEntity>>(
            () => new GenericRepository<TEntity>(context, eventPublisher));
    }
    
    public void Save() => _context.SaveChanges();
    
    public async Task SaveAsync() => await _context.SaveChangesAsync();

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
            await _context.DisposeAsync();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}