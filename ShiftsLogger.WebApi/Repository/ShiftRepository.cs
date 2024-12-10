using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
{
    public ShiftRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<IEnumerable<Shift>> GetAllShiftsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(s => s.StartTime)
            .ToListAsync();

    public async Task<Shift?> GetShiftByIdAsync(Guid shiftId, bool trackChanges) =>
        await FindByCondition(s => s.Id.Equals(shiftId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateShift(Shift shift) =>
        Create(shift);

    public async Task<IEnumerable<Shift>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(s => ids.Contains(s.Id), trackChanges)
            .ToListAsync();

    public void DeleteShift(Shift shift) =>
        Delete(shift);
    
    public void UpdateShift(Shift shift) =>
        Update(shift);

    public bool ShiftExists(Guid shiftId) => 
        Exists(s => s.Id.Equals(shiftId));
}