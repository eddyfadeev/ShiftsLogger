using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp.Controllers;

public class ShiftTypesController
{
    private readonly GenericApiService _userApiService;
    private readonly IApiEndpointMapper _endpointMapper;

    public ShiftTypesController(GenericApiService userApiService, IApiEndpointMapper endpointMapper)
    {
        _userApiService = userApiService;
        _endpointMapper = endpointMapper;
    }

    internal async Task<ShiftType> GetShiftTypeById(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.ShiftTypes.ActionById, id);
        var result = await _userApiService.GetEntityAsync<ShiftType>(url);

        return result;
    }

    internal async Task<List<ShiftType>> GetAllShiftTypes()
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.ShiftTypes.GetAll);
        var result = await _userApiService.GetAllAsync<ShiftType>(url);

        return result;
    }

    internal async Task<List<Shift>> GetShiftsByShiftTypeId(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.ShiftTypes.GetShiftsByShiftTypeId, id);
        var result = await _userApiService.GetAllShiftsByEntityFilterAsync(url);

        return result;
    }
}