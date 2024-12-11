using NLog;
using ShiftsLogger.API;

var builder = await CreateHostBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

await builder.Build().RunAsync();
return;

static async Task<IHostBuilder> CreateHostBuilder(string[] args) =>
    await Task.Run(() => 
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }));