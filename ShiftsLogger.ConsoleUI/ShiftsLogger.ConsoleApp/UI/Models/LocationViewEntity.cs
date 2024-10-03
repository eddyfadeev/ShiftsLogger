using ShiftsLogger.View.Interfaces.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public record LocationViewEntity : IViewModelEntity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }
    public int ElementHeight => 2;
}