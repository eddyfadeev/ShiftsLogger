using Entities.Models;
using Shared.Dto.Shift;

namespace Shared.Mappers;

public static class ShiftMapperExtensions
{
    public static ShiftDto MapToDto(this Shift shift) =>
        new()
        {
            Id = shift.Id,
            User = string.Join(' ', shift.User?.FirstName, shift.User?.LastName),
            Location = shift.Location?.Name ?? string.Empty,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            ShiftType = shift.ShiftType?.Name ?? string.Empty,
            HoursWorked = shift.HoursWorked,
            Description = shift.Description ?? string.Empty
        };
    
    public static Shift MapToEntity(this ShiftForCreationDto shift) =>
        new()
        {
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Description = shift.Description,
            LocationId = shift.LocationId,
            ShiftTypeId = shift.ShiftTypeId,
            UserId = shift.UserId
        };

    public static Shift UpdateEntity(this Shift shift, ShiftForUpdateDto updateDto)
    {
        shift.StartTime = updateDto.StartTime ?? shift.StartTime;
        shift.EndTime = updateDto.EndTime ?? shift.EndTime;

        shift.Description =
            string.IsNullOrWhiteSpace(updateDto.Description)
                ? shift.Description
                : updateDto.Description;

        shift.LocationId = updateDto.LocationId ?? shift.LocationId;
        shift.ShiftTypeId = updateDto.ShiftTypeId ?? shift.ShiftTypeId;
        shift.UserId = updateDto.UserId ?? shift.UserId;

        return shift;
    }
}