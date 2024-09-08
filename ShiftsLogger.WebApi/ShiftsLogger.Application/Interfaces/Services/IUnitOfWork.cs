using ShiftsLogger.Application.Interfaces.Data.Repositories;

namespace ShiftsLogger.Application.Interfaces.Services;

public interface IUnitOfWork<TEntity>
    where TEntity : class
{
    IGenericRepository<TEntity>? Repository { get; }
    void Save();
}