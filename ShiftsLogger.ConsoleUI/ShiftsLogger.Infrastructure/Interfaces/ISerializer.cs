using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Infrastructure.Interfaces;

public interface ISerializer
{
    internal TEntity Deserialize<TEntity>(string json, params Newtonsoft.Json.JsonConverter[] converters);
    internal string Serialize<TEntity>(TEntity entity)
        where TEntity : class, IReportModel;

    internal Newtonsoft.Json.JsonConverter GetConverter<TEntity>()
        where TEntity : class, IReportModel;
}