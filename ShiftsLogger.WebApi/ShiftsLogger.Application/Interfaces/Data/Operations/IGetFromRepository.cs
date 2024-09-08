namespace ShiftsLogger.Application.Interfaces.Data.Operations;

public interface IGetFromRepository<out T>
{
    T? Get(int entityId);
}