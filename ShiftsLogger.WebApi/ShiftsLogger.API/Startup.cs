using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using ShiftsLogger.API.Extensions;
using ShiftsLogger.Presentation;

namespace ShiftsLogger.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) =>
        _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureCors();
        services.ConfigureIisIntegration();
        services.ConfigureLoggerService();
        services.ConfigureRepositoryManager();
        services.ConfigureServiceManager();
        services.ConfigureSqlContext(_configuration);
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        services.AddControllers()
            .AddJsonOptions(options => 
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            .AddApplicationPart(typeof(AssemblyReference).Assembly);
        
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(opt => { });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShiftsLogger API");
            c.RoutePrefix = string.Empty;
        });
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }
        
        app.UseRouting();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}