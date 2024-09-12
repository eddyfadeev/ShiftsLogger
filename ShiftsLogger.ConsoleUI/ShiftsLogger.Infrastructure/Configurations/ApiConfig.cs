namespace ShiftsLogger.Infrastructure.Configurations;

public class ApiConfig
{
    public string? BaseUrl { get; init; }
    public Dictionary<string, string> Locations { get; init; } = new();
    public Dictionary<string, string> ShiftTypes { get; init; } = new();
    public Dictionary<string, string> Users { get; init; } = new();
    public Dictionary<string, string> Shifts { get; init; } = new();
}