using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Domain.Events;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IDbModel
{
    private readonly ShiftsLoggerDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IEventPublisher _eventPublisher;

    public GenericRepository(ShiftsLoggerDbContext context, IEventPublisher eventPublisher)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
        _eventPublisher = eventPublisher;
    }
    
    public virtual async Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        var query = BuildQuery(filter, includeProperties);
        return orderBy is not null ? 
            await orderBy(query).ToListAsync() : 
            await query.ToListAsync();
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(int idToFind)
    {
        var entity = await _dbSet.FindAsync(idToFind);
        DetachIfTracked(entity);
        return entity;
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(int idToFind, string includeProperties)
    {
        var query = BuildQuery(null, includeProperties);
        var entity = await query.FirstOrDefaultAsync(e => e.Id == idToFind);
        DetachIfTracked(entity);
        return entity;
    }

    public virtual async Task InsertAsync(TEntity entityToInsert)
    {
        ArgumentNullException.ThrowIfNull(entityToInsert);
        await _dbSet.AddAsync(entityToInsert);
        await _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToInsert));
    }

    public virtual async Task DeleteAsync(object idToDelete)
    {
        var entityToDelete = await _dbSet.FindAsync(idToDelete);
        ArgumentNullException.ThrowIfNull(entityToDelete);
        Delete(entityToDelete);
        await _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToDelete));
    }

    public virtual async Task UpdateAsync(TEntity entityToUpdate)
    {
        ArgumentNullException.ThrowIfNull(entityToUpdate);
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToUpdate));
    }
    
    public virtual async Task<bool> ExistsAsync(int id, Expression<Func<TEntity, bool>>? filter = null)
    {
        var query = BuildQuery(filter);
        return await query.AnyAsync(e => e.Id == id);
    }
    
    private void Delete(TEntity? entityToDelete)
    {
        ArgumentNullException.ThrowIfNull(entityToDelete);
        
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
        _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToDelete));
    }
    
    private IQueryable<TEntity> BuildQuery(
        Expression<Func<TEntity, bool>>? filter = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    private void DetachIfTracked(TEntity? entity)
    {
        if (entity is not null && _context.Entry(entity).State != EntityState.Detached)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}