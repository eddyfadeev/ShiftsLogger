using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entities;

public class ShiftType : IReportModelWithId
{
    public int Id { get; init; }
    public string Name { get; init; }
}