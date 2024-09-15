using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Infrastructure.Services;

public class ShiftsService
{
    private readonly IRequestHandler _handler;
    private readonly ISerializer _serializer;

    public ShiftsService(IRequestHandler requestHandler, ISerializer serializer)
    {
        _handler = requestHandler;
        _serializer = serializer;
    }

    public async Task<Shift> GetShiftsAsync(Uri uri)
    {
        var response = await _handler.GetAsync(uri);
        var shift = _serializer.Deserialize<Shift>(response);

        return shift;
    }

    public async Task<List<Shift>> GetAllShiftsAsync(Uri uri)
    {
        var response = await _handler.GetAsync(uri);
        var shifts = _serializer.Deserialize<List<Shift>>(response);

        return shifts;
    }
}