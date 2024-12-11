namespace Shared.Dto;

public record LocationDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
}