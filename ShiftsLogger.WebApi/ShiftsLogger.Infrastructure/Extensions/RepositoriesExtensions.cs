using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Infrastructure.Data.Repositories;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class RepositoriesExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IShiftsRepository, ShiftsRepository>();
        services.AddScoped<ILocationsRepository, LocationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IShiftTypesRepository, ShiftTypesRepository>();
    }
}