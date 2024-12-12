using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Service.Contracts;
using Shared.Dto;
using Shared.Mapper;
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

    // TODO: Should I move it to the LocationService???
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

    public async Task<ShiftDto> GetShiftByIdAsync(Guid shiftId, bool trackChanges)
    {
        var shift = await _repository.Shift.GetShiftByIdAsync(shiftId, trackChanges);
        
        if (shift is null) 
        {
            throw new ShiftNotFoundException(shiftId);
        }
        
        return shift.MapToDto();
    }
}