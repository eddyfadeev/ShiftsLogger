using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Infrastructure.Data.Repositories;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class RepositoriesExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Shift>, GenericRepository<Shift>>();
        services.AddScoped<IGenericRepository<ShiftType>, GenericRepository<ShiftType>>();
        services.AddScoped<IGenericRepository<Location>, GenericRepository<Location>>();
        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
    }
}