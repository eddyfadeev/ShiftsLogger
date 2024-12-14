namespace Shared.Dto.User;

public record UserDto : UserForManipulationDto
{
    public Guid Id { get; init; }
}