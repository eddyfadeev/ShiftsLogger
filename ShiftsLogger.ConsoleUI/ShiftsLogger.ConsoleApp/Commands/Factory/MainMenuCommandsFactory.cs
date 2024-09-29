using System.Collections.Frozen;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Commands.MainMenu;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.Factory;

public class MainMenuCommandsFactory : ICommandFactory<MainMenuOptions>
{
    private readonly FrozenDictionary<MainMenuOptions, Func<ICommand>> _factory;

    private readonly IRenderService _renderService;
    private readonly IPanelBuilderService _panelBuilderService;
    private readonly LocationsController _locationsController;
    private readonly UserController _userController;

    public MainMenuCommandsFactory(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        LocationsController locationsController,
        UserController userController
        )
    {
        _factory = InitializeFactory().ToFrozenDictionary();

        _renderService = renderService;
        _panelBuilderService = panelBuilderService;
        _locationsController = locationsController;
        _userController = userController;
    }
    
    public ICommand Create(MainMenuOptions commandToCreate) => 
        _factory[commandToCreate].Invoke();

    private Dictionary<MainMenuOptions, Func<ICommand>> InitializeFactory() =>
        new()
        {
            { MainMenuOptions.AllShifts, () => new AllShiftsCommand() },
            { MainMenuOptions.ShiftsByUser, 
                () => new ShiftsByUserCommand(_renderService, _panelBuilderService, _userController) },
            { MainMenuOptions.ShiftsByType, () => new ShiftsByTypeCommand() },
            { MainMenuOptions.ShiftsByLocation, 
                () => new ShiftsByLocationCommand(_renderService, _panelBuilderService, _locationsController) },
            { MainMenuOptions.Exit, () => new ExitCommand() }
        };
}