using Newtonsoft.Json;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Infrastructure.JsonConverter;

namespace ShiftsLogger.Infrastructure.Services;

public class JsonSerializerService : ISerializer
{
    public TEntity Deserialize<TEntity>(string json, params Newtonsoft.Json.JsonConverter[] converters) =>
        JsonConvert.DeserializeObject<TEntity>(json, converters);

    public string Serialize<TEntity>(TEntity entity) where TEntity : class, IReportModel =>
        JsonConvert.SerializeObject(entity);

    public Newtonsoft.Json.JsonConverter GetConverter<TEntity>()
        where TEntity : class, IReportModel =>
        new ShiftsByEntityReportModelConverter<TEntity>();
}