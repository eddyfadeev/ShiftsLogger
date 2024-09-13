using Newtonsoft.Json;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Mappers;

namespace ShiftsLogger.Domain.Models.Entities;

[JsonConverter(typeof(ShiftTypeMapper))]
public class ShiftType : IReportModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}