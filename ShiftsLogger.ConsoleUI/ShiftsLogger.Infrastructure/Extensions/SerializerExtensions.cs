using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Infrastructure.Interfaces;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class SerializerExtensions
{
    public static void RegisterSerializers(this IServiceCollection services)
    {
        services.AddScoped<ISerializer, JsonSerializerService>();
    }
}