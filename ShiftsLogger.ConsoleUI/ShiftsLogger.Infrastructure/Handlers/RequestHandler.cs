using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Mappers;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Services;
using ShiftsLogger.Infrastructure.Strategies.ConvertStrategy;

namespace ShiftsLogger.Infrastructure.Handlers;

public abstract class RequestHandler
{
    private protected readonly IHttpManager _httpManager;
    private protected abstract IEntityConverter Converter { get; }

    protected RequestHandler(IHttpManager httpManager)
    {
        _httpManager = httpManager;
    }
    
    public abstract Task<GenericReportModel<TEntity>> GetAllAsync<TEntity>(Uri uri) 
        where TEntity : class, IReportModel;

    public async Task<TEntity> GetAsync<TEntity>(Uri uri)
        where TEntity : class, IReportModel
    {
        var response = await _httpManager.GetAsync(uri);
        var entity = Converter.ConvertToEntity<TEntity>(response);

        return entity;
    }
    public virtual async Task CreateAsync(Uri uri, IReportModel entity)
    {
        string jsonContent = Converter.ConvertFromEntity(entity);
        
        await _httpManager.PostAsync(uri, jsonContent);
    }

    public virtual async Task DeleteAsync(Uri uri, int id) => await _httpManager.DeleteAsync(uri, id);
}

public class UserRequestHandler : RequestHandler
{
    public UserRequestHandler(IHttpManager httpManager) : base(httpManager)
    {
    }

    public override async Task<GenericReportModel<TUser>> GetAllAsync<TUser>(Uri uri)
    {
        var response =  await _httpManager.GetAsync(uri);
        var users = Converter.ConvertToEntity<GenericReportModel<TUser>>(response);

        return users;
    }

    private protected override IEntityConverter Converter => new EntityConverter(new UserConverterStrategy());
}