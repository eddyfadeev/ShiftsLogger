namespace Shared.Dto.Location;

public record LocationDto : LocationForManipulationDto
{
    public Guid Id { get; init; }
}