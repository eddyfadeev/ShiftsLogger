using System.Reflection;
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using Service.Contracts;

namespace ShiftsLogger.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIisIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });
    
    public static void ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ShiftsLogger API",
                Description = "A simple API to log worked shifts using ASP.NET Core Web API",
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();
    
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManger>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    public static void ConfigureOutputCaching(this IServiceCollection services) =>
        services.AddOutputCache(options =>
        {
            options.AddPolicy("15MinsExpiry", p => p.Expire(TimeSpan.FromMinutes(15)));
        });
}