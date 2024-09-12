using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entity;

public class User : IDbModel
{
    public int Id { get; init; }
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; init; }
    public string? LastName { get; init; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; init; }
    
    public string? Role { get; init; }
    
    //Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; init; }
}