using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;
using Entities.Validators;

namespace Entities.Models.Entity;

public class Shift : IDbModel
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
    
    public decimal HoursWorked => (decimal)(EndTime - StartTime).TotalHours;

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
}