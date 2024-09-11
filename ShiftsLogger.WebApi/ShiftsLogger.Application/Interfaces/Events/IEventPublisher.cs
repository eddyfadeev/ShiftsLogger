using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Application.Interfaces.Events;

public interface IEventPublisher
{
    Task PublishInteractionEvent<TEntity>(TEntity entity)
        where TEntity : class, IDbModel;
}