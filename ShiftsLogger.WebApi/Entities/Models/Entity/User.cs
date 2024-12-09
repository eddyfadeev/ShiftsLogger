using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities.Models.Entity;

public sealed class User : IDbModel, IEquatable<User>
{
    [Column("UserId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(60, ErrorMessage = "Maximum length for the First Name is 60 characters.")]
    public string? FirstName { get; init; }
    
    [MaxLength(60, ErrorMessage = "Maximum length for the Last Name is 60 characters.")]
    public string? LastName { get; init; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Email is 60 characters.")]
    public string? Email { get; init; }
    
    [MaxLength(60, ErrorMessage = "Maximum length for the Role is 60 characters.")]
    public string? Role { get; init; }
    
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