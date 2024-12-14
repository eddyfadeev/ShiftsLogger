using System.Runtime.Serialization;

namespace Shared.Dto;

[DataContract]
public record ShiftDto
{
    private readonly string? _description;
    
    [DataMember(Order = 1)] 
    public Guid Id { get; init; }
    [DataMember(Order = 2)] 
    public required string User { get; init; }
    [DataMember(Order = 3)] 
    public required string Location { get; init; }
    [DataMember(Order = 4)] 
    public DateTime StartTime { get; init; }
    [DataMember(Order = 5)] 
    public DateTime EndTime { get; init; }
    [DataMember(Order = 6)] 
    public required string ShiftType { get; init; }
    [DataMember(Order = 7)] 
    public decimal HoursWorked { get; init; }
    public string? Description 
    { 
        get => _description;
        init => _description = value ?? string.Empty;
    }
}