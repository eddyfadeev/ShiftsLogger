using System.Runtime.Serialization;

namespace Shared.Dto.Shift;


public record ShiftDto : ShiftForManipulationDto
{
    [DataMember(Order = 1)] 
    public Guid Id { get; init; }
}