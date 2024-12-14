using Contracts;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.Dto.ShiftType;
using Shared.Mappers;
using Shared.RequestFeatures;

namespace Service;

internal sealed class ShiftTypeService : IShiftTypeService
{
    private readonly IRepositoryManager _repository;

    public ShiftTypeService(IRepositoryManager repository) =>
        _repository = repository;


    public async Task<(List<ShiftTypeDto> shiftTypes, PaginationMetaData metaData)> GetAllShiftTypesAsync(
        ShiftTypeParameters queryParameters, bool trackChanges)
    {
        var shiftTypes = await _repository.ShiftType.GetAllShiftTypesAsync(queryParameters, trackChanges);

        var dtos = shiftTypes.Select(st => st.MapToDto()).ToList();

        return (dtos, shiftTypes.PaginationMetaData);
    }

    public async Task<ShiftTypeDto> GetShiftTypeByIdAsync(Guid shiftTypeId, bool trackChanges)
    {
        var shiftType = await TryGetShiftTypeEntity(shiftTypeId, trackChanges);

        return shiftType!.MapToDto();
    }

    public async Task<ShiftTypeDto> CreateShiftTypeAsync(ShiftTypeForCreationDto shiftType)
    {
        var entity = shiftType.MapToEntity();
        
        _repository.ShiftType.CreateShiftType(entity);
        await _repository.SaveAsync();

        return entity.MapToDto();
    }

    public async Task DeleteShiftTypeAsync(Guid shiftTypeId, bool trackChanges)
    {
        var entity = await TryGetShiftTypeEntity(shiftTypeId, trackChanges);
        
        _repository.ShiftType.DeleteShiftType(entity!);
        await _repository.SaveAsync();
    }

    public async Task UpdateShiftTypeAsync(Guid shiftTypeId, ShiftTypeForUpdateDto shiftTypeForUpdate,
        bool trackChanges)
    {
        var entity = await TryGetShiftTypeEntity(shiftTypeId, trackChanges);
        
        entity?.UpdateEntity(shiftTypeForUpdate);
        await _repository.SaveAsync();
    }

    private async Task<ShiftType?> TryGetShiftTypeEntity(Guid shiftTypeId, bool trackChanges) =>
        _repository.ShiftType.ShiftTypeExists(shiftTypeId)
            ? await _repository.ShiftType.GetShiftTypeByIdAsync(shiftTypeId, trackChanges)
            : throw new ShiftTypeNotFoundException(shiftTypeId);
}