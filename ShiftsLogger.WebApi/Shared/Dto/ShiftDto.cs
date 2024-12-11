namespace Shared.Dto;

public record ShiftDto
{
    public Guid Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }

    public decimal HoursWorked { get; init; }

    private readonly string? _description;
    public string? Description 
    { 
        get => _description;
        init => _description = value ?? string.Empty;
    }
}