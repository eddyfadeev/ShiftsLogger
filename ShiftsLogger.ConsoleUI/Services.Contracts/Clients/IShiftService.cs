using Shared.Dto.Shift;
using Shared.RequestFeatures;

namespace Services.Contracts.Clients;

public interface IShiftService
{
    Task<ShiftDto?> GetShiftByIdAsync(Guid shiftId);
    Task<PagedList<ShiftDto>> GetAllShiftsAsync(ShiftParameters? requestParameters = null);
    Task<ShiftDto?> CreateShiftAsync(ShiftDto shift);
    Task DeleteShiftAsync(Guid shiftId);
    Task UpdateShiftAsync(Guid shiftId, ShiftDto shift);
}