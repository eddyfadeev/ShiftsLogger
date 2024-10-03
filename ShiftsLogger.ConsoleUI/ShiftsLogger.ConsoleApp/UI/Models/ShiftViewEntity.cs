using ShiftsLogger.View.Interfaces.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public record ShiftViewEntity : IViewModelEntity
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int LocationId { get; init; }
    public int ShiftTypeId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public decimal HoursWorked { get; init; }
    public string? Description { get; init; }
    public int ElementHeight => 7;
}