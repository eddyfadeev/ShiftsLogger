using Entities.Models;
using Shared.Dto;

namespace Shared.Mappers;

public static class ShiftMapperExtensions
{
    public static ShiftDto MapToDto(this Shift shift) =>
        new()
        {
            Id = shift.Id,
            User = string.Join(' ', shift.User?.FirstName, shift.User?.LastName),
            Location = shift!.Location.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            ShiftType = shift!.ShiftType.Name,
            HoursWorked = shift.HoursWorked,
            Description = shift.Description ?? string.Empty
        };
    
    

    public static Shift MapToEntity(this ShiftDto shift) =>
        new()
        {
            Id = shift.Id,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Description = shift.Description
        };
}