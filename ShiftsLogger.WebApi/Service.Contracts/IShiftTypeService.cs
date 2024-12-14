using Shared.Dto.ShiftType;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IShiftTypeService
{
    Task<(List<ShiftTypeDto> shiftTypes, PaginationMetaData metaData)> 
        GetAllShiftTypesAsync(ShiftTypeParameters queryParameters, bool trackChanges);
    Task<ShiftTypeDto> GetShiftTypeByIdAsync(Guid shiftTypeId, bool trackChanges);
    Task<ShiftTypeDto> CreateShiftTypeAsync(ShiftTypeForCreationDto shiftType);
    Task DeleteShiftTypeAsync(Guid shiftTypeId, bool trackChanges);
    Task UpdateShiftTypeAsync(Guid shiftTypeId, ShiftTypeForUpdateDto updateDto, bool trackChanges);
}