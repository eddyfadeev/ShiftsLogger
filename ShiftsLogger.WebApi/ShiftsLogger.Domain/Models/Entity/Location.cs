using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entity;

public class Location : IDbModel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    
    // Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; init; }
}