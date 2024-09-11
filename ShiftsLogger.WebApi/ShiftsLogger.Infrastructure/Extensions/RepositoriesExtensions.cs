using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces.Data.Repository;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entity;
using ShiftsLogger.Infrastructure.Data.Repository;

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