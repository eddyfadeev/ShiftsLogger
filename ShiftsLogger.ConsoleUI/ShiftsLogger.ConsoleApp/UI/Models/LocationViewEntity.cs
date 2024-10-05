using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class LocationViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }

    public LocationViewEntity()
    {
        SetElementHeight(2);
    }
}