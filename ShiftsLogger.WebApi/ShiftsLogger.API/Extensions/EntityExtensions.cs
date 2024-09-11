using System.Collections;
using System.Collections.ObjectModel;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Dto;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.API.Extensions;

public static class EntityExtensions
{
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

    public static LocationDto MapLocationToDto(this Location location) =>
        new()
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address,
            Shifts = location.Shifts.Select(s => s.MapShiftToDto()).ToList()
        };
}