using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public sealed class User : IEquatable<User>
{
    [Column("UserId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(60, ErrorMessage = "Maximum length for the First Name is 60 characters.")]
    public string? FirstName { get; set; }
    
    [MaxLength(60, ErrorMessage = "Maximum length for the Last Name is 60 characters.")]
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Email is 60 characters.")]
    public string? Email { get; set; }
    
    [MaxLength(60, ErrorMessage = "Maximum length for the Role is 60 characters.")]
    public string? Role { get; set; }
    
    public ICollection<Shift>? Shifts { get; init; }

    public bool Equals(User? other)
    {
        if (other is null)
        {
            return false;
        }
        
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        
        return Id.Equals(other.Id) && 
               FirstName == other.FirstName && 
               LastName == other.LastName && 
               Email == other.Email && 
               Role == other.Role;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        
        return obj.GetType() == GetType() && 
               Equals((User)obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine(Id, FirstName, LastName, Email, Role);
}