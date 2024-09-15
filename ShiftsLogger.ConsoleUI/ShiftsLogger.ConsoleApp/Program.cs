using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Handlers;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.ConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
    
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();
        
        var uriProvider = serviceProvider.GetRequiredService<IApiEndpointMapper>();
        var userService = serviceProvider.GetRequiredService<UserService>();

        var userController = new UserController(userService, uriProvider);
        
        var response1 = userController.GetShiftsByUserId(1).Result;
        var response2 = userController.GetAllUsers().Result;
        var response3 = userController.GetUserById(1).Result;

        
        Console.ReadKey();
    }
}