using Shared.Dto;
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
}