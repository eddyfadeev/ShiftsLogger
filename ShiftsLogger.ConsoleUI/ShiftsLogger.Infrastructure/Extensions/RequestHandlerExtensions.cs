using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Infrastructure.Handlers;
using ShiftsLogger.Infrastructure.Interfaces;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class RequestHandlerExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler, RequestHandler>();
    }
}