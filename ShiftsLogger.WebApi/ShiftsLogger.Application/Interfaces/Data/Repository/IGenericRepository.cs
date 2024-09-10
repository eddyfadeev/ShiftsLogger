using System.Linq.Expressions;

namespace ShiftsLogger.Application.Interfaces.Data.Repository;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    List<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = ""
    );
    TEntity? GetById(object idToFind);
    void Insert(TEntity entityToInsert);
    void Delete(object idToDelete);
    void Delete(TEntity? entityToDelete);
    void Update(TEntity entityToUpdate);
    bool Exists(int id);
    
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    Task<TEntity?> GetByIdAsync(object idToFind);
    Task InsertAsync(TEntity entityToInsert);
    Task DeleteAsync(object idToDelete);
    Task UpdateAsync(TEntity entityToUpdate);
    Task<bool> ExistsAsync(int id, Expression<Func<TEntity, bool>>? filter = null);
}