using System.Runtime.Serialization;

namespace Shared.Dto.Location;

public record LocationDto : LocationForManipulationDto
{
    [DataMember(Order = 1)] 
    public Guid Id { get; init; }
}