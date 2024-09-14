using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Application.Interfaces.Strategies.ConvertStrategy;

public interface IConvertEntityStrategy
{
    string ConvertFromEntity<TEntity>(TEntity entity) where TEntity : class, IReportModel;
    TEntity? ConvertToEntity<TEntity>(string source) where TEntity : class, IReportModel;
}