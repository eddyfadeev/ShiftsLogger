using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Services;

namespace ShiftsLogger.View.Extensions;

public static class ViewExtensions
{
    public static void RegisterViewServices(this IServiceCollection services)
    {
        services.AddTransient<IPanelBuilderService, PanelBuilderService>();
        services.AddScoped<IRenderService, RenderService>();
    }
}