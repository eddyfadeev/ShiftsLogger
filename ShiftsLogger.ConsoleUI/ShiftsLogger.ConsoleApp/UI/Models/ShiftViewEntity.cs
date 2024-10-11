using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class ShiftViewEntity : ViewModelEntity
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int LocationId { get; init; }
    public int ShiftTypeId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public decimal HoursWorked { get; init; }
    public string? Description { get; init; }
    
    public override string GetShortRepresentation()
    {
        SetElementHeight(4);
        return $"""
                {Id}. UserID: {UserId}
                At: {LocationId}
                On: {StartTime:D}
                Hours: {HoursWorked}
                """;
    }

    public override string GetDetailedRepresentation()
    {
        SetElementHeight(6);
        return $"""
                {Id}. UserID: {UserId}
                At: {LocationId}
                Shift type: {ShiftTypeId}
                From: {StartTime:F} To: {EndTime:F}
                Hours: {HoursWorked}
                Description: {Description ?? string.Empty}
                """;
    }

    public override string ToString() =>
        $"""
        {Id}. UserID: {UserId}
        At: {LocationId}
        On: {StartTime:D}
        Hours: {HoursWorked}
        """;
}