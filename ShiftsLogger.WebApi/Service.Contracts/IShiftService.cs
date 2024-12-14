using Shared.Dto.Shift;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IShiftService
{
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges);
    
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForLocation(Guid locationId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForShiftType(Guid shiftTypeId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForUser(Guid userId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<ShiftDto> GetShiftByIdAsync(Guid shiftId, bool trackChanges);
    Task<ShiftDto> CreateShift(Guid shiftId, ShiftForCreationDto shift);
    Task DeleteShift(Guid shiftId, bool trackChanges);
    Task<ShiftDto> UpdateShift(Guid shiftId, ShiftForUpdateDto updateDto, bool trackChanges);
}