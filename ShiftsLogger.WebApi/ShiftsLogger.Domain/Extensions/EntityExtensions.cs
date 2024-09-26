using ShiftsLogger.Domain.Models.Dto;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.Domain.Extensions;

public static class EntityExtensions
{
    public static ShiftDto MapShiftToDto(this Shift shift) =>
        new()
        {
            Id = shift.Id,
            UserId = shift.UserId,
            LocationId = shift.LocationId,
            ShiftTypeId = shift.ShiftTypeId,
            StartTime = Convert.ToDateTime(shift.StartTime.ToString("yyyy-MM-dd HH:mm:ss")),
            EndTime = Convert.ToDateTime(shift.EndTime.ToString("yyyy-MM-dd HH:mm:ss")),
            HoursWorked = shift.HoursWorked,
            Description = shift.Description,
        };
}