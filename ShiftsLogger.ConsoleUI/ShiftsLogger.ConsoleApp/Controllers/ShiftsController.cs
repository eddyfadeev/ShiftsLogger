using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp.Controllers;

public class ShiftsController
{
    private readonly GenericApiService _shiftsApiService;
    private readonly IApiEndpointMapper _endpointMapper;

    public ShiftsController(GenericApiService shiftsApiService, IApiEndpointMapper endpointMapper)
    {
        _shiftsApiService = shiftsApiService;
        _endpointMapper = endpointMapper;
    }

    internal async Task<Shift> GetShiftById(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Shifts.ActionById, id);
        var result = await _shiftsApiService.GetEntityAsync<Shift>(url);

        return result;
    }

    internal async Task<List<Shift>> GetAllShifts()
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Shifts.GetAll);
        var result = await _shiftsApiService.GetAllAsync<Shift>(url);

        return result;
    }

    internal async Task<List<Shift>> GetShiftsByFilterId(Enum filter, int id)
    {
        var filterableEndpoints = GetFiltersEndpoints();

        if (!filterableEndpoints.Contains(filter))
        {
            throw new ArgumentException("Incorrect filter", nameof(filter));
        }

        var uri = _endpointMapper.GetRelativeUrl(filter, id);
        var result = await _shiftsApiService.GetAllShiftsByEntityFilterAsync(uri);

        return result;
    }

    private Enum[] GetFiltersEndpoints() =>
        new Enum[]
        {
            ApiEndpoints.Locations.GetShiftsByLocationId,
            ApiEndpoints.ShiftTypes.GetShiftsByTypeId,
            ApiEndpoints.Users.GetShiftsByUserId
        };
}