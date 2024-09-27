namespace ShiftsLogger.Application.Interfaces;

public interface ICommandFactory<in TEnum>
    where TEnum : Enum
{
    ICommand Create(TEnum commandToCreate);
}