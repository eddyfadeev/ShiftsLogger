namespace ShiftsLogger.Domain.Models.Entity;

public class ShiftLog
{
    public int Id { get; set; }
    public int ShiftId { get; set; }
    public int LocationId { get; set; }
    public int ShiftTypeId { get; set; }

    public Shift Shift { get; set; }
    public Location Location { get; set; }
    public ShiftType ShiftType { get; set; }
}