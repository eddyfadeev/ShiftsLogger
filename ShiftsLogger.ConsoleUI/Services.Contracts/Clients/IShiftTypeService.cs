using Shared.Dto.ShiftType;
using Shared.RequestFeatures;

namespace Services.Contracts.Clients;

public interface IShiftTypeService
{
    Task<ShiftTypeDto?> GetShiftTypeByIdAsync(Guid shiftTypeId);
    Task<PagedList<ShiftTypeDto>> GetAllShiftTypesAsync(ShiftTypeParameters? requestParameters = null);
    Task<ShiftTypeDto?> CreateShiftTypeAsync(ShiftTypeDto shiftType);
    Task DeleteShiftTypeAsync(Guid shiftTypeId);
    Task UpdateShiftTypeAsync(Guid shiftTypeId, ShiftTypeDto shiftType);
}