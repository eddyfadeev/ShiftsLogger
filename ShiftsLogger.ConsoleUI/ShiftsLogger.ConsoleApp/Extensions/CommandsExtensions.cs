using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Commands.Factory;
using ShiftsLogger.Domain.Enums;

namespace ShiftsLogger.ConsoleApp.Extensions;

public static class CommandsExtensions
{
    public static void RegisterFactories(this IServiceCollection services)
    {
        services.AddScoped<ICommandFactory<MainMenuOptions>, MainMenuCommandsFactory>();
    }
}