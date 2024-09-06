namespace ShiftsLogger.Application.Interfaces.Data.Operations;

public interface IAddToRepository<in T>
{
    int Add(T entity);
}