using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Application.Services;

namespace ShiftsLogger.Application.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureAppServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IShiftTypeService, ShiftTypeService>();
    }
}