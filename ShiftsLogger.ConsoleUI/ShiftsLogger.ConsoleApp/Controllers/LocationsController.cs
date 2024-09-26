using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp.Controllers;

public class LocationsController
{
    private readonly GenericApiService _locationsApiService;
    private readonly IApiEndpointMapper _endpointMapper;

    public LocationsController(GenericApiService locationsApiService, IApiEndpointMapper endpointMapper)
    {
        _locationsApiService = locationsApiService;
        _endpointMapper = endpointMapper;
    }

    internal async Task<Location> GetLocationById(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Locations.ActionById, id);
        var result = await _locationsApiService.GetEntityAsync<Location>(url);

        return result;
    }

    internal async Task<List<Location>> GetAllLocations()
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Locations.GetAll);
        var result = await _locationsApiService.GetAllAsync<Location>(url);

        return result;
    }

    internal async Task<List<Shift>> GetShiftsByLocationId(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Locations.GetShiftsByLocationId, id);
        var result = await _locationsApiService.GetAllShiftsByEntityFilterAsync(url);

        return result;
    }
}