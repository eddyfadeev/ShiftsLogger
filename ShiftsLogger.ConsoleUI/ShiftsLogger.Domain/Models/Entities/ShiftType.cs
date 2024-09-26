using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entities;

public class ShiftType : IReportModel
{
    public int Id { get; init; }
    public string EntityName => "Shift Type";
    public string Name { get; init; }

    public override string ToString() => Name;
}