using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entities;

public class Shift : IReportModel
{
    public int Id { get; init; }
    public string EntityName => "Shift";
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

    // TODO: Make it properly
    public override string ToString() =>
        $"""
        {Id}. {UserName ?? $"UserNameId is {UserId}"}  
        Worked as: {UserRole ?? string.Empty}
        At: {LocationName ?? $"locationId is {LocationId}"}
        Worked on: {ShiftTypeDescription ?? $"ShiftTypeId is {ShiftTypeId}"}
        Hours worked: {HoursWorked}
        """;
    
}