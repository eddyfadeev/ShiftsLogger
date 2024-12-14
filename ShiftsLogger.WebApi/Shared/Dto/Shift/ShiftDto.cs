using System.Runtime.Serialization;

namespace Shared.Dto.Shift;


public record ShiftDto : ShiftForManipulationDto
{
    [DataMember(Order = 1)] 
    public Guid Id { get; init; }
    [DataMember(Order = 2)] 
    public required string User { get; init; }
}