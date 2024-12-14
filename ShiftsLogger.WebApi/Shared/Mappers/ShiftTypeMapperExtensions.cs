using Entities.Models;
using Shared.Dto.ShiftType;

namespace Shared.Mappers;

public static class ShiftTypeMapperExtensions
{
    public static ShiftTypeDto MapToDto(this ShiftType shiftType) =>
        new()
        {
            Id = shiftType.Id,
            Name = shiftType!.Name
        };

    public static ShiftType MapToEntity(this ShiftTypeForCreationDto shiftType) =>
        new()
        {
            Name = shiftType.Name
        };

    public static ShiftType UpdateEntity(this ShiftType shiftType, ShiftTypeForUpdateDto updateDto)
    {
        shiftType.Name = string.IsNullOrWhiteSpace(updateDto!.Name)
            ? shiftType.Name
            : updateDto.Name;

        return shiftType;
    }
}