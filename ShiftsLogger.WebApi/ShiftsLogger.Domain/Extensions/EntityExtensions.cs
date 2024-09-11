using ShiftsLogger.Domain.Models.Dto;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.Domain.Extensions;

public static class EntityExtensions
{
    public static ShiftTypeDto MapShiftTypeToDto(this ShiftType shiftType) =>
        new()
        {
            Id = shiftType.Id,
            Name = shiftType.Name
        };

    public static UserDto MapUserToDto(this User user) =>
        new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

    public static LocationDto MapLocationToDto(this Location location) =>
        new()
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address
        };
    
    public static ShiftDto MapShiftToDto(this Shift shift) =>
        new()
        {
            Id = shift.Id,
            UserId = shift.UserId,
            LocationId = shift.LocationId,
            ShiftTypeId = shift.ShiftTypeId,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            HoursWorked = shift.HoursWorked,
            Description = shift.Description ?? "No description provided"
        };
}