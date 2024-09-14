using ShiftsLogger.Application.Interfaces.Strategies.ConvertStrategy;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Strategies.ConvertStrategy;

namespace ShiftsLogger.Infrastructure.Services;

public class EntityConverter : IEntityConverter
{
    private IConvertEntityStrategy _converterStrategy;
    private readonly Dictionary<Type, IConvertEntityStrategy> _converters;

    public EntityConverter(IReportModel reportModel)
    {
        _converters = InitializeConverters();
        _converterStrategy = SetConverter(reportModel.GetType());
    }

    public EntityConverter(IConvertEntityStrategy converterStrategy)
    {
        _converters = InitializeConverters();
        _converterStrategy = converterStrategy;
    }

    public void SetConverterStrategy(IConvertEntityStrategy converterStrategy) =>
        _converterStrategy = converterStrategy;

    public string ConvertFromEntity<TEntity>(TEntity entity) where TEntity : class, IReportModel => 
        _converterStrategy.ConvertFromEntity(entity);
    
    public TEntity? ConvertToEntity<TEntity>(string source) where TEntity : class, IReportModel => 
        _converterStrategy.ConvertToEntity<TEntity>(source);


    private IConvertEntityStrategy SetConverter(Type entityType)
    {
        _converters.TryGetValue(entityType, out var converter);
        
        return converter ?? throw new InvalidOperationException($"No converter found for type {entityType.Name}");
    }

    private static Dictionary<Type, IConvertEntityStrategy> InitializeConverters() =>
        new()
        {
            { typeof(User),  new UserConverterStrategy() },
        };
}

public interface IEntityConverter
{
    void SetConverterStrategy(IConvertEntityStrategy converterStrategy);
    string ConvertFromEntity<TEntity>(TEntity entity) where TEntity : class, IReportModel;
    TEntity? ConvertToEntity<TEntity>(string source) where TEntity : class, IReportModel;
}