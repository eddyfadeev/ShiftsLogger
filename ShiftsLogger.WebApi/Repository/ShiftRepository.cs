using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
{
    public ShiftRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<PagedList<Shift>> GetAllShiftsAsync(ShiftParameters queryParameters, bool trackChanges)
    {
        var shifts = await FindAll(trackChanges)
            .ApplyQueryParametersForRetrieve(queryParameters)
            .ToListAsync();

        var count = await FindAll(trackChanges)
            .ApplyQueryParametersForCount(queryParameters)
            .CountAsync();

        return new PagedList<Shift>
        (
            count, 
            queryParameters.PageNumber, 
            queryParameters.PageSize, 
            shifts
        );
    }

    public async Task<PagedList<Shift>> GetShiftsForLocation(Guid locationId, ShiftParameters queryParameters, bool trackChanges)
    {
        var shifts = await FindByCondition(s => s.LocationId.Equals(locationId), trackChanges)
            .ApplyQueryParametersForRetrieve(queryParameters)
            .ToListAsync();
        
        var count = await FindByCondition(s => s.LocationId.Equals(locationId), trackChanges)
            .ApplyQueryParametersForRetrieve(queryParameters)
            .CountAsync();

        return new PagedList<Shift>(count, queryParameters.PageNumber, queryParameters.PageSize, shifts);
    }
    
    public async Task<PagedList<Shift>> GetShiftsForShiftType(Guid shiftTypeId, ShiftParameters queryParameters, bool trackChanges)
    {
        var shifts = await FindByCondition(s => s.ShiftTypeId.Equals(shiftTypeId), trackChanges)
            .ApplyQueryParametersForRetrieve(queryParameters)
            .ToListAsync();

        var count = await FindByCondition(s => s.ShiftTypeId.Equals(shiftTypeId), trackChanges)
            .ApplyQueryParametersForCount(queryParameters)
            .CountAsync();

        return new PagedList<Shift>(count, queryParameters.PageNumber, queryParameters.PageSize, shifts);
    }
    
    public async Task<PagedList<Shift>> GetShiftsForUser(Guid userId, ShiftParameters queryParameters, bool trackChanges)
    {
        var shifts = await FindByCondition(s => s.UserId.Equals(userId), trackChanges)
            .ApplyQueryParametersForRetrieve(queryParameters)
            .ToListAsync();

        var count = await FindByCondition(s => s.UserId.Equals(userId), trackChanges)
            .ApplyQueryParametersForCount(queryParameters)
            .CountAsync();

        return new PagedList<Shift>(count, queryParameters.PageNumber, queryParameters.PageSize, shifts);
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

    public bool ShiftExists(Guid shiftId) => 
        Exists(s => s.Id.Equals(shiftId));
}