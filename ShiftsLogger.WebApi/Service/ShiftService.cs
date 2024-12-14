using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.Dto.Shift;
using Shared.Mappers;
using Shared.RequestFeatures;

namespace Service;

internal sealed class ShiftService : IShiftService
{
    private readonly IRepositoryManager _repository;

    public ShiftService(IRepositoryManager repository) =>
        _repository = repository;

    public async Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges)
    {
        if (!requestParameters.ValidWorkedHoursRange)
        {
            throw new MaxWorkedHoursRangeBadRequestException();
        }
        
        var shifts = await _repository.Shift.GetAllShiftsAsync(requestParameters, trackChanges);
        
        var dtos = shifts.Select(s => s.MapToDto()).ToList();

        return (dtos, shifts.PaginationMetaData);
    }

    public async Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForLocation(Guid locationId, ShiftParameters requestParameters, bool trackChanges)
    {
        if (!_repository.Location.LocationExists(locationId))
        {
            throw new LocationNotFoundException(locationId);
        }

        var shifts = await _repository.Shift.GetShiftsForLocation(locationId, requestParameters, trackChanges);

        var dtos = shifts.Select(s => s.MapToDto()).ToList();

        return (dtos, shifts.PaginationMetaData);
    }
    
    public async Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForShiftType(Guid shiftTypeId, ShiftParameters requestParameters, bool trackChanges)
    {
        if (!_repository.ShiftType.ShiftTypeExists(shiftTypeId))
        {
            throw new ShiftTypeNotFoundException(shiftTypeId);
        }

        var shifts = await _repository.Shift.GetShiftsForShiftType(shiftTypeId, requestParameters, trackChanges);

        var dtos = shifts.Select(s => s.MapToDto()).ToList();

        return (dtos, shifts.PaginationMetaData);
    }
    
    public async Task<(List<ShiftDto> shifts, PaginationMetaData metaData)> 
        GetShiftsForUser(Guid userId, ShiftParameters requestParameters, bool trackChanges)
    {
        if (!_repository.User.UserExists(userId))
        {
            throw new UserNotFoundException(userId);
        }

        var shifts = await _repository.Shift.GetShiftsForUser(userId, requestParameters, trackChanges);

        var dtos = shifts.Select(s => s.MapToDto()).ToList();

        return (dtos, shifts.PaginationMetaData);
    }

    public async Task<ShiftDto> GetShiftByIdAsync(Guid shiftId, bool trackChanges)
    {
        var shift = await TryGetShiftAsync(shiftId, trackChanges);
        
        return shift.MapToDto();
    }

    public async Task<ShiftDto> CreateShift(Guid shiftId, ShiftForCreationDto shift)
    {
        VerifyLocationId(shift.LocationId);
        VerifyUserId(shift.UserId);
        VerifyShiftTypeId(shift.ShiftTypeId);
        
        var shiftEntity = shift.MapToEntity();
        
        _repository.Shift.CreateShift(shiftEntity);
        await _repository.SaveAsync();
        
        return shiftEntity.MapToDto();
    }

    public async Task DeleteShift(Guid shiftId, bool trackChanges)
    {
        var shift = await TryGetShiftAsync(shiftId, trackChanges);

        _repository.Shift.DeleteShift(shift);
        await _repository.SaveAsync();
    }

    public async Task<ShiftDto> UpdateShift(Guid shiftId, ShiftForUpdateDto updateDto, bool trackChanges)
    {
        VerifyLocationId(updateDto.LocationId);
        VerifyUserId(updateDto.UserId);
        VerifyShiftTypeId(updateDto.ShiftTypeId);
        
        var shift = await TryGetShiftAsync(shiftId, trackChanges);
        shift.UpdateEntity(updateDto);
        await _repository.SaveAsync();

        return shift.MapToDto();
    }
    
    private async Task<Shift> TryGetShiftAsync(Guid shiftId, bool trackChanges) =>
        (_repository.Shift.ShiftExists(shiftId) 
            ? await _repository.Shift.GetShiftByIdAsync(shiftId, trackChanges)
            : throw new ShiftNotFoundException(shiftId))!;

    private void VerifyLocationId(Guid? locationId)
    {
        if (locationId is not null && !_repository.Location.LocationExists(locationId.Value))
        {
            throw new LocationNotFoundException(locationId.Value);
        }
    }

    private void VerifyUserId(Guid? userId)
    {
        if (userId is not null && !_repository.User.UserExists(userId.Value))
        {
            throw new UserNotFoundException(userId.Value);
        }
    }

    private void VerifyShiftTypeId(Guid? shiftTypeId)
    {
        if (shiftTypeId is not null && !_repository.ShiftType.ShiftTypeExists(shiftTypeId.Value))
        {
            throw new ShiftTypeNotFoundException(shiftTypeId.Value);
        }
    }
}