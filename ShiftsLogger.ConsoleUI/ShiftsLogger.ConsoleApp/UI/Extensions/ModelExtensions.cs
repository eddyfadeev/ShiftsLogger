using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.ConsoleApp.UI.Extensions;

public static class ModelExtensions
{
    public static UserViewEntity MapToViewEntity(this User entity) =>
        new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Role = entity.Role
        };
    
    public static LocationViewEntity MapToViewEntity(this Location entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address
        };
    
    public static ShiftTypeViewEntity MapToViewEntity(this ShiftType entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    
    public static ShiftViewEntity MapToViewEntity(this Shift entity) =>
        new()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            LocationId = entity.LocationId,
            ShiftTypeId = entity.ShiftTypeId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            HoursWorked = entity.HoursWorked,
            Description = entity.Description
        };
    
    public static User MapToEntity(this UserViewEntity viewEntity) =>
        new()
        {
            Id = viewEntity.Id,
            FirstName = viewEntity.FirstName,
            LastName = viewEntity.LastName,
            Email = viewEntity.Email,
            Role = viewEntity.Role
        };
    
    public static Location MapToEntity(this LocationViewEntity viewEntity) =>
        new()
        {
            Id = viewEntity.Id,
            Name = viewEntity.Name,
            Address = viewEntity.Address
        };
    
    public static ShiftType MapToEntity(this ShiftTypeViewEntity viewEntity) =>
        new()
        {
            Id = viewEntity.Id,
            Name = viewEntity.Name
        };
    
    public static Shift MapToEntity(this ShiftViewEntity viewEntity) =>
        new()
        {
            Id = viewEntity.Id,
            UserId = viewEntity.UserId,
            LocationId = viewEntity.LocationId,
            ShiftTypeId = viewEntity.ShiftTypeId,
            StartTime = viewEntity.StartTime,
            EndTime = viewEntity.EndTime,
            HoursWorked = viewEntity.HoursWorked,
            Description = viewEntity.Description
        };
}