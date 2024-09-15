using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp.Controllers;

public class ShiftsController
{
    private readonly EntitiesGenericService _shiftsService;
    private readonly IApiEndpointMapper _endpointMapper;

    public ShiftsController(EntitiesGenericService shiftsService, IApiEndpointMapper endpointMapper)
    {
        _shiftsService = shiftsService;
        _endpointMapper = endpointMapper;
    }

    internal async Task<Shift> GetShiftById(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Shifts.ActionById, id);
        var result = await _shiftsService.GetEntityAsync<Shift>(url);

        return result;
    }

    internal async Task<List<Shift>> GetAllShifts()
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Shifts.GetAll);
        var result = await _shiftsService.GetAllAsync<Shift>(url);

        return result;
    }
    
    
}