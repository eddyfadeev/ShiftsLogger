using Microsoft.Extensions.DependencyInjection;
using View.Services;
using View.Services.Contracts;

namespace ViewConfigurator;

public static class ViewConfigurator
{
    public static void AddView(this IServiceCollection services) =>
        services.AddTransient<ITableBuilder, TableBuilderService>();
}