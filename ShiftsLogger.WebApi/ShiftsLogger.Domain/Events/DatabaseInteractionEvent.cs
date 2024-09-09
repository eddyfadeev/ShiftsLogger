using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Events;

public class DatabaseInteractionEvent<TEntity> : IEvent
{
    public TEntity Entity { get; }
    
    public DatabaseInteractionEvent(TEntity entity)
    {
        Entity = entity;
    }
}