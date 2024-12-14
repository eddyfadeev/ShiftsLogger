using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.Repository;

public interface IShiftTypeRepository
{
    Task<PagedList<ShiftType>> GetAllShiftTypesAsync(ShiftTypeParameters queryParameters, bool trackChanges);
    Task<ShiftType?> GetShiftTypeByIdAsync(Guid shiftTypeId, bool trackChanges);
    void CreateShiftType(ShiftType shiftType);
    void DeleteShiftType(ShiftType shiftType);
    bool ShiftTypeExists(Guid shiftTypeId);
}