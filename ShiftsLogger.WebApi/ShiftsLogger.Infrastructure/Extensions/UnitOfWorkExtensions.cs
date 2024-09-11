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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}