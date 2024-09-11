using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models.Entity;

public class User : IDbModel
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
    
    //Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; init; }
}