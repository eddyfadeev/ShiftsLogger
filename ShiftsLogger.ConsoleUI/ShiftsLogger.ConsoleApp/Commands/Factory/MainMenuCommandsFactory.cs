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
    private readonly IPanelBuilder _panelBuilder;
    private readonly LocationsController _locationsController;
    private readonly UserController _userController;
    private readonly ShiftTypesController _shiftTypesController;
    private readonly ShiftsController _shiftsController;

    public MainMenuCommandsFactory(
        IRenderService renderService, 
        IPanelBuilder panelBuilder, 
        LocationsController locationsController,
        UserController userController,
        ShiftTypesController shiftTypesController,
        ShiftsController shiftsController
        )
    {
        _factory = InitializeFactory().ToFrozenDictionary();

        _renderService = renderService;
        _panelBuilder = panelBuilder;
        _locationsController = locationsController;
        _userController = userController;
        _shiftTypesController = shiftTypesController;
        _shiftsController = shiftsController;
    }
    
    public ICommand Create(MainMenuOptions commandToCreate) => 
        _factory[commandToCreate].Invoke();

    private Dictionary<MainMenuOptions, Func<ICommand>> InitializeFactory() =>
        new()
        {
            { MainMenuOptions.AllShifts, 
                () => new AllShiftsMenuCommand(_renderService, _panelBuilder, _shiftsController) },
            { MainMenuOptions.ShiftsByUser, 
                () => new ShiftsByUserCommand(_renderService, _panelBuilder, _userController) },
            { MainMenuOptions.ShiftsByType, () 
                => new ShiftsByTypeCommand(_renderService, _panelBuilder, _shiftTypesController) },
            { MainMenuOptions.ShiftsByLocation, 
                () => new ShiftsByLocationCommand(_renderService, _panelBuilder, _locationsController) },
            { MainMenuOptions.Exit, () => new ExitCommand() }
        };
}