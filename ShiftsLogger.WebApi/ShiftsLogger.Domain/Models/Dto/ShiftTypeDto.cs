namespace ShiftsLogger.Domain.Models.Dto;

public record ShiftTypeDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<ShiftDto> Shifts { get; init; }
}