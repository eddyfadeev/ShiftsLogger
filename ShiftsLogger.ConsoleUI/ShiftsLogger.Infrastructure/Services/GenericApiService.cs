using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Infrastructure.Interfaces;

namespace ShiftsLogger.Infrastructure.Services;

public class GenericApiService
{
    private readonly IRequestHandler _requestHandler;
    private readonly ISerializer _serializer;

    public GenericApiService(IRequestHandler requestHandler, ISerializer serializer)
    {
        _requestHandler = requestHandler;
        _serializer = serializer;
    }

    public async Task<TEntity> GetEntityAsync<TEntity>(Uri uri)
        where TEntity : class, IReportModel
    {
        var response = await _requestHandler.GetAsync(uri);
        var entity = _serializer.Deserialize<TEntity>(response);

        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync<TEntity>(Uri uri)
        where TEntity : class, IReportModel
    {
        var response = await _requestHandler.GetAsync(uri);
        var entities = _serializer.Deserialize<List<TEntity>>(response);

        return entities;
    }

    public async Task<ShiftsByEntityReportModel<TEntity>> GetAllShiftsByEntityTypeAsync<TEntity>(Uri uri)
        where TEntity : class, IReportModel
    {
        var response = await _requestHandler.GetAsync(uri);
        var converter = _serializer.GetConverter<TEntity>();
        var shiftsByEntity = _serializer.Deserialize<ShiftsByEntityReportModel<TEntity>>(response, converter);

        return shiftsByEntity;
    }
}