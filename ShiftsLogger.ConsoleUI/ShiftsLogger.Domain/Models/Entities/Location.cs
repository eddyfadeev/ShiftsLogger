using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entities;

public class Location : IReportModelWithId
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }
}