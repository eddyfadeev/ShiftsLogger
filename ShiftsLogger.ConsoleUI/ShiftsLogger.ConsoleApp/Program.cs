using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.ConsoleUI;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Handlers;
using ShiftsLogger.Infrastructure.Services;
using Terminal.Gui;

namespace ShiftsLogger.ConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
    
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();
        
        Terminal.Gui.Application.Init();
        var win = new MainMenuWindow();
        
        Terminal.Gui.Application.Run(win);
        win.Display();
        win.Dispose();
        Terminal.Gui.Application.Shutdown();

        
        //Console.ReadKey();
    }
}