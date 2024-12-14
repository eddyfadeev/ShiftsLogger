using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ShiftTypeRepository : RepositoryBase<ShiftType>, IShiftTypeRepository
{
    public ShiftTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<PagedList<ShiftType>> GetAllShiftTypesAsync(ShiftTypeParameters queryParameters, bool trackChanges)
    {
        var shiftTypes = await FindAll(trackChanges)
            .Search(queryParameters)
            .Sort(queryParameters)
            .Page(queryParameters)
            .ToListAsync();

        var count = await FindAll(trackChanges)
            .Search(queryParameters)
            .CountAsync();

        return new PagedList<ShiftType>
        (
            count,
            queryParameters.PageNumber,
            queryParameters.PageSize,
            shiftTypes
        );
    }

    public async Task<ShiftType?> GetShiftTypeByIdAsync(Guid shiftTypeId, bool trackChanges) =>
        await FindByCondition(st => st.Id.Equals(shiftTypeId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateShiftType(ShiftType shiftType) =>
        Create(shiftType);

    public void DeleteShiftType(ShiftType shiftType) =>
        Delete(shiftType);

    public bool ShiftTypeExists(Guid shiftTypeId) =>
        Exists(st => st.Id.Equals(shiftTypeId));
}