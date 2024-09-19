using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entities;

public class Shift : IReportModelWithId
{
    public int Id { get; init; }
    
    public int UserId { get; init; }
    public string? UserName { get; init; }
    public string? UserRole { get; init; }
    
    public int LocationId { get; init; }
    public string? LocationName { get; init; }
    
    public int ShiftTypeId { get; init; }    
    public string? ShiftTypeDescription { get; init; }
    
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public decimal HoursWorked { get; init; }
    
    public string? Description { get; init; }

    public override string ToString() =>
        $"""
        {UserName ?? "UserName is null"}  
        Worked as: {UserRole ?? string.Empty}
        At: {LocationName ?? "location is null"}
        Worked on: {ShiftTypeDescription ?? string.Empty}
        Hours worked: {HoursWorked}
        """;
    
}