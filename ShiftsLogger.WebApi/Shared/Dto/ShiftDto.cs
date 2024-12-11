using System.Runtime.Serialization;

namespace Shared.Dto;

[DataContract]
public record ShiftDto
{
    private readonly string? _description;
    
    [DataMember(Order = 1)]
    public Guid Id { get; init; }
    [DataMember(Order = 2)]
    public DateTime StartTime { get; init; }
    [DataMember(Order = 3)]
    public DateTime EndTime { get; init; }
    [DataMember(Order = 4)]
    public decimal HoursWorked { get; init; }
    [DataMember(Order = 5)]
    public string? Description 
    { 
        get => _description;
        init => _description = value ?? string.Empty;
    }
}