using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;
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

    public async Task<List<Shift>> GetAllShiftsByEntityFilterAsync(Uri uri)
    {
        var response = string.Empty;
        
        try
        {
            response = await _requestHandler.GetAsync(uri);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught in GenericApiService. Uri: {uri}");
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.StackTrace}");
            Console.ReadKey();
            return new List<Shift>();
        }
        
        var shiftsByEntity = _serializer.Deserialize<List<Shift>>(response);

        return shiftsByEntity;
    }
}