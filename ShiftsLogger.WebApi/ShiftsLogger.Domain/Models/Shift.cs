using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLogger.Domain.Models;

public class Shift
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LocationId { get; set; }
    public int ShiftTypeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    
    public decimal HoursWorked => (decimal)(EndTime - StartTime).TotalHours;
    
    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
    
    [ForeignKey(nameof(ShiftTypeId))]
    public virtual ShiftType? ShiftType { get; set; }
    
    [ForeignKey(nameof(LocationId))]
    public virtual Location? Location { get; set; }
}