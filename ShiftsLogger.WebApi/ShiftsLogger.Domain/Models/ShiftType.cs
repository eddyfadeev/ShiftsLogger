﻿namespace ShiftsLogger.Domain.Models;

public class ShiftType
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., "Day Shift", "Night Shift", "Overtime"
    
    // Navigation property
    public ICollection<Shift> Shifts { get; set; }
}