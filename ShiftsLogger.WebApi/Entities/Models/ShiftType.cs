using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public sealed class ShiftType : IEquatable<ShiftType>
{
    [Column("ShiftTypeId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Shift type name is required" )]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
    
    public ICollection<Shift>? Shifts { get; init; }

    public bool Equals(ShiftType? other)
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
               Name == other.Name;
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
               Equals((ShiftType)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Shifts);
    }
}