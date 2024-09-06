namespace ShiftsLogger.Application.Interfaces.Data.Operations;

public interface IRemoveFromRepository<in T>
{
    int Remove(T entity);
}