using Entities.Models;

namespace Contracts.Repository;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetAllShiftsAsync(bool trackChanges);
    Task<Shift?> GetShiftByIdAsync(Guid shiftId, bool trackChanges);
    void CreateShift(Shift shift);
    Task<IEnumerable<Shift>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteShift(Shift shift);
    void UpdateShift(Shift shift); 
    bool ShiftExists(Guid shiftId);
}