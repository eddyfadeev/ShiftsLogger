using Newtonsoft.Json;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.JsonConverter;

namespace ShiftsLogger.Infrastructure.Handlers;

public abstract class RequestHandlerBase<TEntity>
    where TEntity : class, IReportModel
{
    private protected readonly IHttpManager _httpManager;

    protected RequestHandlerBase(IHttpManager httpManager)
    {
        _httpManager = httpManager;
    }
    
    public abstract Task<List<TEntity>> GetAllAsync(Uri uri);

    public abstract Task<ShiftsByEntityReportModel<TEntity>> GetShiftsByEntityId(Uri uri);

    public async Task<TEntity> GetAsync(Uri uri)
    {
        var response = await _httpManager.GetAsync(uri);
        var entity = JsonConvert.DeserializeObject<TEntity>(response);

        return entity;
    }
    
    public virtual async Task DeleteAsync(Uri uri, int id) => 
        await _httpManager.DeleteAsync(uri, id);
}

public class UserRequestHandler : RequestHandlerBase<User>
{
    public UserRequestHandler(IHttpManager httpManager) : base(httpManager)
    {
    }


    public override async Task<List<User>> GetAllAsync(Uri uri)
    {
        var response = await _httpManager.GetAsync(uri);
        var users = JsonConvert.DeserializeObject<List<User>>(response);

        return users;
    }

    public override async Task<ShiftsByEntityReportModel<User>> GetShiftsByEntityId(Uri uri)
    {
        var response = await _httpManager.GetAsync(uri);
        var settings = new JsonSerializerSettings()
        {
            Converters = [new ShiftsByEntityReportModelConverter<User>()]
        };
        var shiftsByUser = JsonConvert.DeserializeObject<ShiftsByEntityReportModel<User>>(response, settings);

        return shiftsByUser;
    }
}