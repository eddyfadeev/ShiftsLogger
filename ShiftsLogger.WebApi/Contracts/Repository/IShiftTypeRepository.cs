using Entities.Models.Entity;

namespace Contracts.Repository;

public interface IShiftTypeRepository
{
    Task<IEnumerable<ShiftType>> GetAllShiftTypesAsync(bool trackChanges);
    Task<ShiftType?> GetShiftTypeByIdAsync(Guid shiftTypeId, bool trackChanges);
    void CreateShiftType(ShiftType shiftType);
    void DeleteShiftType(ShiftType shiftType);
    void UpdateShiftType(ShiftType shiftType);
    bool ShiftTypeExists(Guid shiftTypeId);
}