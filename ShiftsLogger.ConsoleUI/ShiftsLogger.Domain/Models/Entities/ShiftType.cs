using Shared.Interfaces;

namespace Shared.Models.Entities;

public class ShiftType : IReportModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}