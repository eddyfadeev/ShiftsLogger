using System.Text.Json.Serialization;
using Shared.Interfaces;
using Shared.Mappers;

namespace Shared.Models.Entities;

[JsonConverter(typeof(ShiftMapper))]
public class Shift : IReportModel
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
}