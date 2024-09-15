using Newtonsoft.Json;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Application.Interfaces;

public interface ISerializer
{
    TEntity Deserialize<TEntity>(string json, params JsonConverter[] converters);

    string Serialize<TEntity>(TEntity entity)
        where TEntity : class, IReportModel;

    JsonConverter GetConverter<TEntity>()
        where TEntity : class, IReportModel;
}