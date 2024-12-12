using Shared.Dto;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IShiftService
{
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges);
    Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForLocation(Guid locationId, ShiftParameters requestParameters, bool trackChanges);
    
    Task<ShiftDto> GetShiftByIdAsync(Guid shiftId, bool trackChanges);
}