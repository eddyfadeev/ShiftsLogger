using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models;

public class ShiftType : IDbModel
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., "Day Shift", "Night Shift", "Overtime"
    
    // Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; set; }
}