using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public sealed class Location : IEquatable<Location>
{
    [Column("LocationId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Location name is required" )]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
    
    [MaxLength(500, ErrorMessage = "Maximum length for the Address is 500 characters.")]
    public string? Address { get; init; }
    
    public ICollection<Shift>? Shifts { get; init; }


    public bool Equals(Location? other)
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
               Name == other.Name && 
               Address == other.Address;
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
               Equals((Location)obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine(Id, Name, Address);
}