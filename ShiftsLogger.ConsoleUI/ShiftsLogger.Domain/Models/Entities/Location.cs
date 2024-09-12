using Newtonsoft.Json;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Mappers;

namespace ShiftsLogger.Domain.Models.Entities;

[JsonConverter(typeof(LocationMapper))]
public class Location : IReportModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }
}