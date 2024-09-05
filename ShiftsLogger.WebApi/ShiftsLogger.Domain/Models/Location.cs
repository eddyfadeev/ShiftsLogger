﻿namespace ShiftsLogger.Domain.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    // Navigation property
    public ICollection<Shift> Shifts { get; set; }
}