﻿using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Infrastructure.Data;
using ShiftsLogger.Infrastructure.Data.Repositories;

namespace ShiftsLogger.Infrastructure.Services;

public sealed class UnitOfWork<TEntity> : IDisposable, IUnitOfWork<TEntity>
    where TEntity : class
{
    private readonly ShiftsLoggerDbContext _context;
    private readonly Lazy<IGenericRepository<TEntity>> _repository;
    private bool _disposed = false;

    public IGenericRepository<TEntity> Repository =>
        _repository.Value;
    
    public UnitOfWork(ShiftsLoggerDbContext context)
    {
        _context = context;
        _repository = new Lazy<IGenericRepository<TEntity>>(
            () => new GenericRepository<TEntity>(context));
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
}