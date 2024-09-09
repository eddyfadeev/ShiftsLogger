using System.Text.Json.Serialization;

namespace ShiftsLogger.Domain.Models;

public class ShiftType
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., "Day Shift", "Night Shift", "Overtime"
    
    // Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift> Shifts { get; set; }
}