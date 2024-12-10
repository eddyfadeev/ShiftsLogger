using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ShiftTypeRepository : RepositoryBase<ShiftType>, IShiftTypeRepository
{
    public ShiftTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<IEnumerable<ShiftType>> GetAllShiftTypesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(st => st.Name)
            .ToListAsync();

    public async Task<ShiftType?> GetShiftTypeByIdAsync(Guid shiftTypeId, bool trackChanges) =>
        await FindByCondition(st => st.Id.Equals(shiftTypeId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateShiftType(ShiftType shiftType) =>
        Create(shiftType);

    public void DeleteShiftType(ShiftType shiftType) =>
        Delete(shiftType);

    public void UpdateShiftType(ShiftType shiftType) =>
        Update(shiftType);

    public bool ShiftTypeExists(Guid shiftTypeId) =>
        Exists(st => st.Id.Equals(shiftTypeId));
}