using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Handlers;

namespace ShiftsLogger.ConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
    
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();
        
        var uriProvider = serviceProvider.GetRequiredService<IApiEndpointMapper>();
        var getAllUsers = uriProvider.GetRelativeUrl(ApiEndpoints.Users.GetAll);
        var getShiftsByUserId = uriProvider.GetRelativeUrl(ApiEndpoints.Users.GetShiftsByUserId, 1);
        var getUserById = uriProvider.GetRelativeUrl(ApiEndpoints.Users.ActionById, 1);
        
        var manager = serviceProvider.GetRequiredService<IHttpManager>();
        var userRequestHandler = new UserRequestHandler(manager); 
        
        var response1 = userRequestHandler.GetShiftsByEntityId(getShiftsByUserId).Result;
        var response2 = userRequestHandler.GetAllAsync(getAllUsers).Result;
        var response3 = userRequestHandler.GetAsync(getUserById).Result;

        
        Console.ReadKey();
    }
}