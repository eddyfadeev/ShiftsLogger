using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class LocationViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }

    public LocationViewEntity()
    {
        ElementHeight = 2;
    }

    public override string GetShortRepresentation() => ToString();

    public override string GetDetailedRepresentation() => ToString();

    public override string ToString() =>
        $"""
        {Id}. {Name}
        {Address ?? string.Empty}
        """;
}