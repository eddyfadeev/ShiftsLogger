namespace Shared.Dto.Shift;

public record ShiftForCreationDto : ShiftForManipulationDto
{
    public required Guid UserId { get; set; }
    public required Guid LocationId { get; set; }
    public required Guid ShiftTypeId { get; set; }
}