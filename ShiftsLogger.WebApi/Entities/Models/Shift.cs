using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Validators;

namespace Entities.Models;

public sealed class Shift : IEquatable<Shift>
{
    [Column("ShiftId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Shift start time is required")]
    [DataType(DataType.DateTime)]
    [ShiftDateValidator]
    public DateTime StartTime { get; init; }
    
    [Required(ErrorMessage = "Shift end time is required")]
    [DataType(DataType.DateTime)]
    [ShiftDateValidator]
    public DateTime EndTime { get; init; }
    
    [MaxLength(2000, ErrorMessage = "Description can't be longer than 2000 characters")]
    public string? Description { get; init; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column(TypeName = "decimal(18,2)")]
    [Description("Computed by the db column for hours worked")]
    public decimal HoursWorked { get; set; }

    #region Foreign Relations

    [ForeignKey(nameof(User))]
    public Guid UserId { get; init; }
    public User? User { get; init; }
    
    [ForeignKey(nameof(Location))]
    public Guid LocationId { get; init; }
    public Location? Location { get; init; }
    
    [ForeignKey(nameof(ShiftType))]
    public Guid ShiftTypeId { get; init; }
    public ShiftType? ShiftType { get; init; }

    #endregion

    public bool Equals(Shift? other)
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
               StartTime.Equals(other.StartTime) && 
               EndTime.Equals(other.EndTime) && 
               Description == other.Description && 
               HoursWorked.Equals(other.HoursWorked) &&
               UserId.Equals(other.UserId) && 
               LocationId.Equals(other.LocationId) && 
               ShiftTypeId.Equals(other.ShiftTypeId);
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
               Equals((Shift)obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine
        (
            Id, StartTime, EndTime, Description, 
            HoursWorked, UserId, LocationId, ShiftTypeId
        );
}