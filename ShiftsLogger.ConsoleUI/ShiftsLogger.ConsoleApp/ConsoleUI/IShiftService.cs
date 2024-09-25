namespace ShiftsLogger.ConsoleApp.ConsoleUI;

public interface IShiftService
{
    Dictionary<string, List<string>> GetShiftsByLocation();
}