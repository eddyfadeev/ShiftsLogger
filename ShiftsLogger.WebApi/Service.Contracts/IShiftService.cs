using Shared.Dto.Shift;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IShiftService
{
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges);
    
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForLocationAsync(Guid locationId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForShiftTypeAsync(Guid shiftTypeId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForUserAsync(Guid userId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<ShiftDto> GetShiftByIdAsync(Guid shiftId, bool trackChanges);
    Task<ShiftDto> CreateShiftAsync(ShiftForCreationDto shift);
    Task DeleteShiftAsync(Guid shiftId, bool trackChanges);
    Task<ShiftDto> UpdateShiftAsync(Guid shiftId, ShiftForUpdateDto updateDto, bool trackChanges);
}