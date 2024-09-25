using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.ConsoleApp.ConsoleUI;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.ConsoleApp;

public static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        var locationsController = serviceProvider.GetRequiredService<LocationsController>();

        var renderService = new RenderService();

        var locations = locationsController.GetAllLocations().Result;
        var shiftsByLocation = new Dictionary<Location, List<Shift>>();
        foreach (var location in locations)
        {
            shiftsByLocation.Add(
                location, locationsController.GetShiftsByLocationId(location.Id).Result);
        }

        var shiftsView = new ShiftsView<Location>(locations, shiftsByLocation);

        renderService.RenderLayout(shiftsView);

        while (true)
        {
            var key = Console.ReadKey(true).Key;

            shiftsView = HandleUserInput(shiftsView, key);
            renderService.RenderLayout(shiftsView);
        }
    }

    private static ShiftsView<Location> HandleUserInput(ShiftsView<Location> shiftsView, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                shiftsView.ChangeSelection(Selection.MoveUp);
                break;
            case ConsoleKey.DownArrow:
                shiftsView.ChangeSelection(Selection.MoveDown);
                break;
            case ConsoleKey.LeftArrow:
                shiftsView.ChangeSelection(Selection.MoveLeft);
                break;
            case ConsoleKey.RightArrow:
                shiftsView.ChangeSelection(Selection.MoveRight);
                break;
            case ConsoleKey.Enter:
                shiftsView.ChangeSelection(Selection.Select);
                break;
        }

        return shiftsView;
    }
}