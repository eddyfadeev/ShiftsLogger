using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp.Controllers;

public class UserController
{
    private readonly GenericApiService _userApiService;
    private readonly IApiEndpointMapper _endpointMapper;

    public UserController(GenericApiService userApiService, IApiEndpointMapper endpointMapper)
    {
        _userApiService = userApiService;
        _endpointMapper = endpointMapper;
    }

    internal async Task<User> GetUserById(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Users.ActionById, id);
        var result = await _userApiService.GetEntityAsync<User>(url);

        return result;
    }

    internal async Task<List<User>> GetAllUsers()
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Users.GetAll);
        var result = await _userApiService.GetAllAsync<User>(url);

        return result;
    }

    internal async Task<ShiftsByEntityReportModel<User>> GetShiftsByUserId(int id)
    {
        var url = _endpointMapper.GetRelativeUrl(ApiEndpoints.Users.GetShiftsByUserId, id);
        var result = await _userApiService.GetAllShiftsByEntityTypeAsync<User>(url);

        return result;
    }
}