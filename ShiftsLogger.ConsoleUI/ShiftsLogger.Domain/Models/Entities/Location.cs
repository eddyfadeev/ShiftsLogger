using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Mappers;

namespace Shared.Models.Entities;

[JsonConverter(typeof(LocationMapper))]
public class Location : IReportModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; init; }
}