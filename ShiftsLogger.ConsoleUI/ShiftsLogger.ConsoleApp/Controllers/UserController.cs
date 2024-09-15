using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp.Controllers;

public class UserController
{
    private readonly EntitiesGenericService _userService;
    private readonly IApiEndpointMapper _endpointMapper;

    public UserController(EntitiesGenericService userService, IApiEndpointMapper endpointMapper)
    {
        _userService = userService;
        _endpointMapper = endpointMapper;
    }

    internal async Task<User> GetUserById(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Users.ActionById, id);
        var result = await _userService.GetEntityAsync<User>(url);

        return result;
    }

    internal async Task<List<User>> GetAllUsers()
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Users.GetAll);
        var result = await _userService.GetAllAsync<User>(url);

        return result;
    }

    internal async Task<ShiftsByEntityReportModel<User>> GetShiftsByUserId(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Users.GetShiftsByUserId, id);
        var result = await _userService.GetAllShiftsByEntityTypeAsync<User>(url);

        return result;
    }
}