using Shared.Interfaces;

namespace Shared.Models.Entities;

public class Location : IReportModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
}