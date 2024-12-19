using Shared.RequestFeatures;

namespace Contracts;

public interface IHttpClientBase<TEntity>
{
    Task<TEntity?> GetByIdAsync(Uri endpoint);
    Task<PagedList<TEntity>?> GetAllAsync(Uri endpoint);
    Task<TEntity?> CreateAsync(Uri endpoint, TEntity entity);
    Task UpdateAsync(Uri endpoint, TEntity entity);
    Task DeleteAsync(Uri uri);
}