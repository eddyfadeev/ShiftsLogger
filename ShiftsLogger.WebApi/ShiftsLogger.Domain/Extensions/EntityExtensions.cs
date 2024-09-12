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
            LastName = user.LastName ?? string.Empty,
            Email = user.Email,
            Role = user.Role ?? "No role provided",
        };

    public static LocationDto MapLocationToDto(this Location location) =>
        new()
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address ?? "No address provided",
        };
    
    public static ShiftDto MapShiftToDto(this Shift shift) =>
        new()
        {
            Id = shift.Id,
            UserId = shift.UserId,
            UserName = shift.User?.FirstName + " " + (shift.User?.LastName ?? string.Empty),
            UserRole = shift.User?.Role ?? "No role provided",
            LocationId = shift.LocationId,
            LocationName = shift.Location?.Name!,
            ShiftTypeId = shift.ShiftTypeId,
            ShiftTypeDescription = shift.ShiftType?.Name!,
            StartTime = Convert.ToDateTime(shift.StartTime.ToString("yyyy-MM-dd HH:mm")),
            EndTime = Convert.ToDateTime(shift.EndTime.ToString("yyyy-MM-dd HH:mm")),
            HoursWorked = shift.HoursWorked,
            Description = shift.Description ?? "No description provided",
        };
}