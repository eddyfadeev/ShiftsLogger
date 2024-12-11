using Entities.Models;
using Shared.Dto;

namespace Shared.Mapper;

public static class MapperExtensions
{
    public static ShiftDto MapToDto(this Shift shift) =>
        new()
        {
            Id = shift.Id,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
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