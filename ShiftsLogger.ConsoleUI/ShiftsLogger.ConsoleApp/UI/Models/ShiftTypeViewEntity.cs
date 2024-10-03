using ShiftsLogger.View.Interfaces.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public record ShiftTypeViewEntity : IViewModelEntity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int ElementHeight => 1;
}