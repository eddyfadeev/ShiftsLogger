using Newtonsoft.Json;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Mappers;

namespace ShiftsLogger.Domain.Models.Entities;

[JsonConverter(typeof(UserMapper))]
public class User : IReportModel
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; }
    public string? Role { get; init; }
}