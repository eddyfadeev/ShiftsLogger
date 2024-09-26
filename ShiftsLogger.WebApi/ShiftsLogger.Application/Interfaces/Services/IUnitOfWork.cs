using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Application.Interfaces.Services;

public interface IUnitOfWork
    
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IDbModel;
    Task CompleteAsync();
}