namespace ShiftsLogger.Application.Interfaces.Data.Operations;

public interface IGetAllFromRepository<T>
{
    List<T> GetAll();
}