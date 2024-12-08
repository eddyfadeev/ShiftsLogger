using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities.Models.Entity;

public class ShiftType : IDbModel
{
    [Column("ShiftTypeId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Shift type name is required" )]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
    
    public virtual ICollection<Shift>? Shifts { get; init; }
}