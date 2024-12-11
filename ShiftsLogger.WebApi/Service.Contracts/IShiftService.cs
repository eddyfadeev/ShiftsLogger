using Shared.Dto;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IShiftService
{
    Task<(List<ShiftDto> shifts, MetaData metaData)> GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges);

    Task<(List<ShiftDto> shifts, MetaData metaData)> GetShiftsForLocation(Guid locationId,
        ShiftParameters requestParameters, bool trackChanges);
}