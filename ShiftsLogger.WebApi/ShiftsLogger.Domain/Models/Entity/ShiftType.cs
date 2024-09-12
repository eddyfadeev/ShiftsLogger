using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entity;

public class ShiftType : IDbModel
{
    public int Id { get; init; }
    [Required(ErrorMessage = "Shift type name is required" )]
    public string Name { get; init; }
    
    // Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; init; }
}