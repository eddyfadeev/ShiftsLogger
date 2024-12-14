using Entities.Models;
using Shared.Dto.Location;

namespace Shared.Mappers;

public static class LocationMapperExtensions
{
    public static LocationDto MapToDto(this Location location) =>
        new()
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address ?? string.Empty
        };

    public static Location MapToEntity(this LocationForCreationDto location) =>
        new()
        {
            Name = location.Name,
            Address = location.Address,
        };
    
    public static Location UpdateEntity(this Location location, LocationForUpdateDto updateDto)
    {
        location.Name = string.IsNullOrWhiteSpace(updateDto.Name) ? location.Name : updateDto.Name;
        location.Address= string.IsNullOrWhiteSpace(updateDto.Address) ? location.Address : updateDto.Address;
        
        return location;
    }
}