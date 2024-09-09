using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Domain.Events;
using ShiftsLogger.Domain.Interfaces;

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

    public virtual List<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (string includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy is not null ? 
            orderBy(query).ToList() : 
            query.ToList();
    }
    
    public virtual TEntity? GetById(object idToFind) => 
        _dbSet.Find(idToFind);
    
    public virtual void Insert(TEntity entityToInsert)
    {
        ArgumentNullException.ThrowIfNull(entityToInsert);
        _dbSet.Add(entityToInsert);
        
        _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToInsert));
    }

    public virtual void Delete(object idToDelete)
    {
        TEntity? entityToDelete = _dbSet.Find(idToDelete);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity? entityToDelete)
    {
        ArgumentNullException.ThrowIfNull(entityToDelete);
        
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
        
        _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToDelete));
    }
    
    public virtual void Update(TEntity entityToUpdate)
    {
        ArgumentNullException.ThrowIfNull(entityToUpdate);
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        
        _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToUpdate));
    }
    
    public virtual bool Exists(int id) => 
        _context.Find<TEntity>(id) is not null;
    
    public virtual async Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy is not null ? 
            await orderBy(query).ToListAsync() : 
            await query.ToListAsync();
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(object idToFind)
    {
        var entity = await _dbSet.FindAsync(idToFind);
        if (entity is not null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
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
        TEntity? entityToDelete = await _dbSet.FindAsync(idToDelete);
        ArgumentNullException.ThrowIfNull(entityToDelete);
        
        await _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToDelete));
        
        Delete(entityToDelete);
    }

    public virtual async Task UpdateAsync(TEntity entityToUpdate)
    {
        ArgumentNullException.ThrowIfNull(entityToUpdate);
        
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        
        await _eventPublisher.PublishAsync(new DatabaseInteractionEvent<TEntity>(entityToUpdate));
        
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task<bool> ExistsAsync(int id) =>
        await _context.FindAsync<TEntity>(id) is not null;
}