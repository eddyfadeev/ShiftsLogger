using ShiftsLogger.API;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
Startup.Configure(app, app.Environment);

await app.RunAsync();