using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class ServicesExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<ShiftsService>();
    }
}