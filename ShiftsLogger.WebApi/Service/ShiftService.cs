using Contracts;
using Entities.Exceptions.BadRequest;
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

    public async Task<(List<ShiftDto> shifts, MetaData metaData)> GetAllShiftsAsync(ShiftParameters requestParameters, bool trackChanges)
    {
        if (!requestParameters.ValidWorkedHoursRange)
        {
            throw new MaxWorkedHoursRangeBadRequestException();
        }
        
        var shifts = await _repository.Shift.GetAllShiftsAsync(requestParameters, trackChanges);
        
        var dtos = shifts.Select(s => s.MapToDto()).ToList();

        return (dtos, shifts.MetaData);
    }
}