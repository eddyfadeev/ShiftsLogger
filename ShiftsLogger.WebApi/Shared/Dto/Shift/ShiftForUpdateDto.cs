namespace Shared.Dto.Shift;

public record ShiftForUpdateDto : ShiftForManipulationDto
{
    public Guid? UserId { get; set; }
    public Guid? LocationId { get; set; }
    public Guid? ShiftTypeId { get; set; }
    public new DateTime? StartTime { get; init; }
    public new DateTime? EndTime { get; init; }
}