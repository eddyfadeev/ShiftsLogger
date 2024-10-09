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

    public ShiftViewEntity()
    {
        SetElementHeight(5);
    }
    
    public override string GetShortRepresentation()
    {
        throw new NotImplementedException();
    }

    public override string GetDetailedRepresentation()
    {
        throw new NotImplementedException();
    }

    public override string ToString() =>
        $"""
        {Id}. UserID: {UserId}
        At: {LocationId}
        On: {StartTime:D}
        Hours: {HoursWorked}
        """;
}