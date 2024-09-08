using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Application.Interfaces.Data.Repositories;

namespace ShiftsLogger.Infrastructure.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly ShiftsLoggerDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ShiftsLoggerDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

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
    
    public virtual TEntity? GetById(object idToFind) => _dbSet.Find(idToFind);
    
    public virtual void Insert(TEntity entityToInsert) => _dbSet.Add(entityToInsert);

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
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}