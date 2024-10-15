using System.Collections.Frozen;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.UI.Commands.Factory;

public class MainMenuCommandsFactory : ICommandFactory<MainMenuOptions>
{
    private readonly FrozenDictionary<MainMenuOptions, Func<ICommand>> _factory;

    private readonly IRenderService _renderService;
    private readonly IPanelBuilderService _panelBuilderService;
    private readonly LocationsController _locationsController;
    private readonly UserController _userController;
    private readonly ShiftTypesController _shiftTypesController;
    private readonly ShiftsController _shiftsController;

    public MainMenuCommandsFactory(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        LocationsController locationsController,
        UserController userController,
        ShiftTypesController shiftTypesController,
        ShiftsController shiftsController
        )
    {
        _factory = InitializeFactory().ToFrozenDictionary();

        _renderService = renderService;
        _panelBuilderService = panelBuilderService;
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
                () => new OpenAllShifts(_renderService, _panelBuilderService, _shiftsController) },
            { MainMenuOptions.ShiftsByUser, 
                () => new OpenShiftsByUser(_renderService, _panelBuilderService, _userController) },
            { MainMenuOptions.ShiftsByType, () 
                => new OpenShiftsByType(_renderService, _panelBuilderService, _shiftTypesController) },
            { MainMenuOptions.ShiftsByLocation, 
                () => new OpenShiftsByLocation(_renderService, _panelBuilderService, _locationsController) },
            { MainMenuOptions.Exit, () => new ExitFromApp() }
        };
}