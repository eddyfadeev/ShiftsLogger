using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    private readonly IServiceProvider _serviceProvider;
    
    public EventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent 
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
        
        foreach (var handler in handlers)
        {
            await handler.HandleAsync(@event);
        }
    }
}