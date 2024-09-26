using Newtonsoft.Json;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Infrastructure.Interfaces;

namespace ShiftsLogger.Infrastructure.Services;

public class JsonSerializerService : ISerializer
{
    public TEntity Deserialize<TEntity>(string json) =>
        JsonConvert.DeserializeObject<TEntity>(json);

    public string Serialize<TEntity>(TEntity entity) where TEntity : class, IReportModel =>
        JsonConvert.SerializeObject(entity);
}