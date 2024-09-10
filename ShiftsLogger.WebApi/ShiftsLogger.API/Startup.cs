using System.Text.Json.Serialization;
using ShiftsLogger.API.Extensions;
using ShiftsLogger.API.Middleware;
using ShiftsLogger.Infrastructure.Extensions;

namespace ShiftsLogger.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureCors();
        services.ConfigureIisIntegration();
        services.ConfigureDbContext(_configuration);
        services.ConfigureRepositories();
        services.ConfigureUnitOfWork();
        services.ConfigureEvents();
        
        services.AddControllers()
            .AddJsonOptions(options => 
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseCors();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}