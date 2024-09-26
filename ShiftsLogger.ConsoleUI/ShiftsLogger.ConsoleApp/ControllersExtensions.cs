using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.ConsoleApp.Controllers;

namespace ShiftsLogger.ConsoleApp;

public static class ControllersExtensions
{
    public static void RegisterControllers(this IServiceCollection services)
    {
        services.AddScoped<LocationsController>();
        services.AddScoped<ShiftsController>();
    }
}