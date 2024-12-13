using Entities.Models;
using Shared.Dto;

namespace Shared.Mapper;

public static class MapperExtensions
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
    
    public static LocationDto MapToDto(this Location location) =>
        new()
        {
            Id = location.Id,
            Name = location.Name ?? string.Empty,
            Address = location.Address ?? string.Empty
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