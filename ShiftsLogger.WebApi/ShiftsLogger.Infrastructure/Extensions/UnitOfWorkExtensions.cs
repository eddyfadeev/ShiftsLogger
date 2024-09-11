using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entity;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class UnitOfWorkExtensions
{
    public static void ConfigureUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<Shift>, UnitOfWork<Shift>>();
        services.AddScoped<IUnitOfWork<ShiftType>, UnitOfWork<ShiftType>>();
        services.AddScoped<IUnitOfWork<Location>, UnitOfWork<Location>>();
        services.AddScoped<IUnitOfWork<User>, UnitOfWork<User>>();
    }
}