using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Application.Services;
using ShiftsLogger.Infrastructure.Data;
using ShiftsLogger.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShiftsLoggerContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IShiftsLoggerRepository, ShiftsLoggerRepository>();
builder.Services.AddScoped<IShiftsLoggerService, ShiftLoggerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
