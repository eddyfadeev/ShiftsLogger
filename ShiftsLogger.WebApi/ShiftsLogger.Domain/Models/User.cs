﻿using System.Text.Json.Serialization;

namespace ShiftsLogger.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    
    //Navigation property
    [JsonIgnore]
    public virtual ICollection<Shift> Shifts { get; set; }
}