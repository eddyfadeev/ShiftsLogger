namespace Shared.Dto.Shift;

public record ShiftForCreationDto : ShiftForManipulationDto
{
    public required Guid UserId { get; init; }
    public required Guid LocationId { get; init; }
    public required Guid ShiftTypeId { get; init; }
}