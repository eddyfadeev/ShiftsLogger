using System.Text.Json.Serialization;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models;

public class User : IDbModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    
    //Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift>? Shifts { get; set; }
}