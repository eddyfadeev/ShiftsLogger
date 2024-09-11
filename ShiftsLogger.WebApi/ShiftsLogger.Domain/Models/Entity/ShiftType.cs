using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entity;

public class ShiftType : IDbModel
{
    public int Id { get; init; }
    public string Name { get; init; } // e.g., "Day Shift", "Night Shift", "Overtime"
    
    // Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; init; }
}