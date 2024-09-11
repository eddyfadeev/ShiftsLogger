namespace ShiftsLogger.Domain.Models.Dto;

public record LocationDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public List<ShiftDto> Shifts { get; init; }
}