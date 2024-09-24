namespace ShiftsLogger.Domain.Models.Dto;

public record ShiftDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int LocationId { get; init; }
    public int ShiftTypeId { get; init; }    
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public decimal HoursWorked { get; init; }
    public string Description { get; init; }
}