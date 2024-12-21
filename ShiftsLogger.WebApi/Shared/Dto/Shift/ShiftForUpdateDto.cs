namespace Shared.Dto.Shift;

public record ShiftForUpdateDto : ShiftForManipulationDto
{
    public Guid? UserId { get; init; }
    public Guid? LocationId { get; init; }
    public Guid? ShiftTypeId { get; init; }
    public new DateTime? StartTime { get; init; }
    public new DateTime? EndTime { get; init; }
}