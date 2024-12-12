using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.Repository;

public interface IShiftRepository
{
    Task<PagedList<Shift>> GetAllShiftsAsync(ShiftParameters queryParameters, bool trackChanges);
    Task<PagedList<Shift>> GetShiftsForLocation(Guid locationId, ShiftParameters queryParameters, bool trackChanges);
    Task<PagedList<Shift>> GetShiftsForShiftType(Guid shiftTypeId, ShiftParameters queryParameters, bool trackChanges);
    Task<PagedList<Shift>> GetShiftsForUser(Guid userId, ShiftParameters queryParameters, bool trackChanges);
    Task<Shift?> GetShiftByIdAsync(Guid shiftId, bool trackChanges);
    void CreateShift(Shift shift);
    Task<IEnumerable<Shift>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteShift(Shift shift);
    void UpdateShift(Shift shift); 
    bool ShiftExists(Guid shiftId);
}