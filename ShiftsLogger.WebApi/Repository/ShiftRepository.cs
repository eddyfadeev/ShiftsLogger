using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
{
    public ShiftRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<PagedList<Shift>> GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges)
    {
        var shifts = await FindAll(trackChanges)
            .Filter(requestParameters)
            .Search(requestParameters)
            .Sort(requestParameters)
            .Skip((requestParameters.PageNumber - 1) * requestParameters.PageSize)
            .Take(requestParameters.PageSize)
            .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Shift>(count, requestParameters.PageNumber, requestParameters.PageSize, shifts);
    }

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