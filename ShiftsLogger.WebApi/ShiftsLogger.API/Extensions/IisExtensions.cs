namespace ShiftsLogger.API.Extensions;

public static class IisExtensions
{
    public static void ConfigureIisIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });
}