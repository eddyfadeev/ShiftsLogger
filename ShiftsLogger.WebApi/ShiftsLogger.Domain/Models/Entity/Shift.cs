using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Validators;

namespace ShiftsLogger.Domain.Models.Entity;

public class Shift : IDbModel
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int LocationId { get; init; }
    public int ShiftTypeId { get; init; }
    
    [Required(ErrorMessage = "Shift start time is required")]
    [DataType(DataType.DateTime)]
    [ShiftDateValidator]
    public DateTime StartTime { get; init; }
    
    [Required(ErrorMessage = "Shift end time is required")]
    [DataType(DataType.DateTime)]
    [ShiftDateValidator]
    public DateTime EndTime { get; init; }
    
    public string? Description { get; init; }
    
    public decimal HoursWorked => (decimal)(EndTime - StartTime).TotalHours;
    
    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; init; }
    
    [ForeignKey(nameof(ShiftTypeId))]
    public virtual ShiftType? ShiftType { get; init; }
    
    [ForeignKey(nameof(LocationId))]
    public virtual Location? Location { get; init; }
}