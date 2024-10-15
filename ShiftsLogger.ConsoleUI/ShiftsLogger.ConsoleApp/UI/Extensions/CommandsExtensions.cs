using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.UI.Commands.Factory;
using ShiftsLogger.Domain.Enums;

namespace ShiftsLogger.ConsoleApp.UI.Extensions;

public static class CommandsExtensions
{
    public static void RegisterFactories(this IServiceCollection services)
    {
        services.AddScoped<ICommandFactory<MainMenuOptions>, MainMenuCommandsFactory>();
    }
}