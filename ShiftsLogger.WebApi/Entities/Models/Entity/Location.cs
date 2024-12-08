using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities.Models.Entity;

public class Location : IDbModel
{
    [Column("LocationId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Location name is required" )]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
    
    [MaxLength(500, ErrorMessage = "Maximum length for the Address is 500 characters.")]
    public string? Address { get; init; }
    
    public ICollection<Shift>? Shifts { get; init; }
}