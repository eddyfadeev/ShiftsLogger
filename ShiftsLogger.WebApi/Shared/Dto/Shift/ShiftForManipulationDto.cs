using System.Runtime.Serialization;

namespace Shared.Dto.Shift;

[DataContract]
public abstract record ShiftForManipulationDto
{
    private readonly string _description;
    private readonly  string _location;
    private readonly string _shiftType;

    [DataMember(Order = 3)]
    public virtual string? Location
    {
        get => _location; 
        init => _location = value ?? string.Empty;
    }
    
    [DataMember(Order = 4)] 
    public DateTime StartTime { get; init; }
    [DataMember(Order = 5)] 
    public DateTime EndTime { get; init; }

    [DataMember(Order = 6)]
    public string? ShiftType
    {
        get => _shiftType; 
        init => _shiftType = value ?? string.Empty;
    }
    
    [DataMember(Order = 7)] 
    public decimal HoursWorked { get; init; }
    
    [DataMember(Order = 8)]
    public string? Description 
    { 
        get => _description;
        init => _description = value ?? string.Empty;
    }
}