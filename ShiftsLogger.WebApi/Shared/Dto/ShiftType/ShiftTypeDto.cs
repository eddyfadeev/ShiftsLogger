namespace Shared.Dto.ShiftType;

public record ShiftTypeDto : ShiftTypeForManipulationDto
{
    public Guid Id { get; init; }
}