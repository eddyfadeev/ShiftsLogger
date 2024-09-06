namespace ShiftsLogger.Application.Interfaces.Data.Operations;

public interface IUpdateInRepository<in T>
{
    int Update(T entity);
}