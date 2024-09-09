using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models;

public class Location : IDbModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    // Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; set; }
}