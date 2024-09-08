using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Application.Services;
using ShiftsLogger.Infrastructure.Data;
using ShiftsLogger.Infrastructure.Data.Repositories;

namespace ShiftsLogger.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder
                    .AllowAnyOrigin()   // Can use as WithOrigins("https://example.com")
                    .AllowAnyMethod()   // Can change to WithMethods("POST", "GET")
                    .AllowAnyHeader()); // Can configure headers WithHeaders("accept", "content-type")
        });

    // To configure IIS 
    public static void ConfigureIisIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ShiftsLoggerDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    public static void ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "ShiftsLogger API",
                Description = "A simple API to log worked shifts using ASP.NET Core Web API",
            });
        });
    
    public static void ConfigureAppServices(this IServiceCollection services)
    {
        services.AddScoped<IShiftsRepository, ShiftsRepository>();
        services.AddScoped<ILocationsRepository, LocationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IShiftTypesRepository, ShiftTypesRepository>();
        
        services.AddScoped<IShiftsLoggerService, ShiftsLoggerService>();
    }
}
