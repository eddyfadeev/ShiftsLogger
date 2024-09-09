using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Events.Handlers;
using ShiftsLogger.Application.Interfaces.Events;
using ShiftsLogger.Domain.Events;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Infrastructure.Events;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class EventsExtensions
{
    public static void ConfigureEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddTransient<IEventHandler<DatabaseInteractionEvent<Shift>>, ShiftInteractionEventHandler>();
    }
}