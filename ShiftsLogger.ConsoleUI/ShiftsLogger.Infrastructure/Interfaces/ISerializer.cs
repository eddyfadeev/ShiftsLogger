using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Infrastructure.Interfaces;

public interface ISerializer
{
    internal TEntity Deserialize<TEntity>(string json);
    internal string Serialize<TEntity>(TEntity entity)
        where TEntity : class, IReportModel;
}