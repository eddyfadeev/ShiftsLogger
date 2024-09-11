using System.Linq.Expressions;

namespace ShiftsLogger.Application.Interfaces.Data.Repository;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    Task<TEntity?> GetByIdAsync(int idToFind);
    Task<TEntity?> GetByIdAsync(int idToFind, string includeProperties);
    Task InsertAsync(TEntity entityToInsert);
    Task DeleteAsync(object idToDelete);
    Task UpdateAsync(TEntity entityToUpdate);
    Task<bool> ExistsAsync(int id, Expression<Func<TEntity, bool>>? filter = null);
}