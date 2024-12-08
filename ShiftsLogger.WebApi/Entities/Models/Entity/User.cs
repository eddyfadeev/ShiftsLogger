using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities.Models.Entity;

public class User : IDbModel
{
    [Column("UserId")]
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(60, ErrorMessage = "Maximum length for the First Name is 60 characters.")]
    public string? FirstName { get; init; }
    
    [MaxLength(60, ErrorMessage = "Maximum length for the Last Name is 60 characters.")]
    public string? LastName { get; init; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Email is 60 characters.")]
    public string? Email { get; init; }
    
    [MaxLength(60, ErrorMessage = "Maximum length for the Role is 60 characters.")]
    public string? Role { get; init; }
    
    public virtual ICollection<Shift>? Shifts { get; init; }
}