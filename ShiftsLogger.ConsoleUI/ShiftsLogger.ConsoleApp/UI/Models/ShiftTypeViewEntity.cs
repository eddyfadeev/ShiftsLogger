using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class ShiftTypeViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public string Name { get; init; }

    public ShiftTypeViewEntity()
    {
        ElementHeight = 1;
    }

    public override string GetShortRepresentation() => ToString();

    public override string GetDetailedRepresentation() => ToString();

    public override string ToString() =>
        $"{Id}. {Name}";
}