using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Infrastructure.Data;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ShiftsLoggerDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
}