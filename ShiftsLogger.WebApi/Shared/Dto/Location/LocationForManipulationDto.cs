using System.Runtime.Serialization;

namespace Shared.Dto.Location;

public abstract record LocationForManipulationDto
{
    private string _name;
    private string _address;
    
    [DataMember(Order = 2)]
    public string? Name
    {
        get => _name;
        init => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }

    [DataMember(Order = 3)] 
    public string? Address
    {
        get => _address;
        init => _address = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }
}