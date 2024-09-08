using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Infrastructure.Data;
using ShiftsLogger.Infrastructure.Data.Repositories;

namespace ShiftsLogger.Infrastructure.Services;

public sealed class UnitOfWork<TEntity> : IDisposable, IUnitOfWork<TEntity>
    where TEntity : class
{
    private readonly ShiftsLoggerDbContext _context;
    private IGenericRepository<TEntity>? _repository;
    
    private bool disposed = false;

    public IGenericRepository<TEntity> Repository => 
        _repository ??= 
            new GenericRepository<TEntity>(_context);
    
    public UnitOfWork(ShiftsLoggerDbContext context)
    {
        _context = context;
    }
    
    public void Save() => _context.SaveChanges();

    private void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}