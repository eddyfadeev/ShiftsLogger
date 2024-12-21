using System.Runtime.Serialization;

namespace Shared.Dto.Shift;

[DataContract]
public abstract record ShiftForManipulationDto
{
    private string _user;
    private string _description;
    private string _location;
    private string _shiftType;

    [DataMember(Order = 3)]
    public string? User
    {
        get => _user;
        init => _user = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }

    [DataMember(Order = 3)]
    public string? Location
    {
        get => _location; 
        init => _location = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
    
    [DataMember(Order = 4)] 
    public DateTime StartTime { get; init; }
    
    [DataMember(Order = 5)] 
    public DateTime EndTime { get; init; }

    [DataMember(Order = 6)]
    public string? ShiftType
    {
        get => _shiftType; 
        init => _shiftType = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
    
    [DataMember(Order = 7)] 
    public decimal HoursWorked { get; init; }
    
    [DataMember(Order = 8)]
    public string? Description 
    { 
        get => _description;
        init => _description = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
}