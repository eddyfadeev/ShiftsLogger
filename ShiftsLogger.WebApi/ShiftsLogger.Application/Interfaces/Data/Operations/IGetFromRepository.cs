namespace ShiftsLogger.Application.Interfaces.Data.Operations;

public interface IGetFromRepository<T>
{
    T? Get(T entity);
}