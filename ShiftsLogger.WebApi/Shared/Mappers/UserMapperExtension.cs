using Entities.Models;
using Shared.Dto.User;

namespace Shared.Mappers;

public static class UserMapperExtension
{
    public static UserDto MapToDto(this User user) =>
        new()
        {
            Id = user.Id,
            FirstName = user.FirstName ?? "Error mapping first name",
            LastName = user.LastName ?? string.Empty,
            Email = user.Email ?? "Error mapping email",
            Role = user.Role ?? string.Empty
        };

    public static User MapToEntity(this UserForCreationDto user) =>
        new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };

    public static User UpdateEntity(this User user, UserForUpdateDto updateDto)
    {
        user.FirstName = string.IsNullOrWhiteSpace(updateDto.FirstName)
            ? user.FirstName
            : updateDto.FirstName;
        
        user.LastName = string.IsNullOrWhiteSpace(updateDto.LastName)
            ? user.LastName
            : updateDto.LastName;
        
        user.Email = string.IsNullOrWhiteSpace(updateDto.Email)
            ? user.Email
            : updateDto.Email;
        
        user.Role = string.IsNullOrWhiteSpace(updateDto.Role)
            ? user.Role
            : updateDto.Role;

        return user;
    }
}