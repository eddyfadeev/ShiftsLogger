using System.ComponentModel.DataAnnotations;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Domain.Validators;

public class ShiftDateValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var shift = (Shift)validationContext.ObjectInstance;

        if (shift.StartTime > shift.EndTime)
        {
            return new ValidationResult("Start time cannot be later than end time.");
        }
        
        if (shift.StartTime > DateTime.Now)
        {
            return new ValidationResult("Shift start date cannot be in the future.");
        }

        if (shift.EndTime > DateTime.Now)
        {
            return new ValidationResult("End time cannot be in the future.");
        }
        
        return ValidationResult.Success;
    }
}