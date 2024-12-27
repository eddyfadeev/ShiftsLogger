using Microsoft.Extensions.DependencyInjection;
using View.Contracts.Services;
using View.TableBuilderService;
using ViewConfigurator.Factory;

namespace ViewConfigurator;

public static class ViewConfigurator
{
    public static void AddView(this IServiceCollection services)
    {
        services.AddTransient<ITableBuilder, TableBuilder>();
        services.AddTransient<IMenuFactory, MenuFactory>();
    }
}