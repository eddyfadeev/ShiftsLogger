using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Infrastructure.Services;

public class UserService
{
    private readonly IRequestHandler _handler;
    private readonly ISerializer _serializer;

    public UserService(IRequestHandler requestHandler, ISerializer serializer)
    {
        _handler = requestHandler;
        _serializer = serializer;
    }

    public async Task<User> GetUserAsync(Uri uri)
    {
        var response = await _handler.GetAsync(uri);
        var user = _serializer.Deserialize<User>(response);

        return user;
    }

    public async Task<List<User>> GetAllUsersAsync(Uri uri)
    {
        var response = await _handler.GetAsync(uri);
        var users = _serializer.Deserialize<List<User>>(response);

        return users;
    }

    public async Task<ShiftsByEntityReportModel<User>> GetShiftsByUser(Uri uri)
    {
        var response = await _handler.GetAsync(uri);
        var converter = _serializer.GetConverter<User>();
        var shiftsByUser = _serializer.Deserialize<ShiftsByEntityReportModel<User>>(response, converter);

        return shiftsByUser;
    }
}