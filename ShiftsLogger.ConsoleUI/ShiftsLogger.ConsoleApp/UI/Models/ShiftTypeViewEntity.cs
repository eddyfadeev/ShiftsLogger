using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class ShiftTypeViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public string Name { get; init; }

    public ShiftTypeViewEntity()
    {
        SetElementHeight(1);
    }
}