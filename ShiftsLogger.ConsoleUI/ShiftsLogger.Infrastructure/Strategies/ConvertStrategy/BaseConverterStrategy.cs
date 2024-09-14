using Newtonsoft.Json;
using ShiftsLogger.Application.Interfaces.Strategies.ConvertStrategy;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Infrastructure.Strategies.ConvertStrategy;

public abstract class BaseConverterStrategy : IConvertEntityStrategy
{
    private readonly JsonSerializerSettings _settings;

    protected BaseConverterStrategy()
    {
        _settings = GetJsonSettings();
    }

    public virtual string ConvertFromEntity<TEntity>(TEntity entity)
        where TEntity : class, IReportModel =>
        JsonConvert.SerializeObject(entity, _settings);

    public virtual TEntity? ConvertToEntity<TEntity>(string source)
        where TEntity : class, IReportModel =>
        JsonConvert.DeserializeObject<TEntity>(source, _settings);

    private JsonSerializerSettings GetJsonSettings() =>
        new()
        {
            Converters = GetJsonConverters(),
            NullValueHandling = NullValueHandling.Ignore
        };

    private protected abstract List<JsonConverter> GetJsonConverters();
}