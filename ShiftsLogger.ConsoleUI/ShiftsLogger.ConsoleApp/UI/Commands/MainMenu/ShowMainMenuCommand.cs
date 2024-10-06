using System.Collections.Frozen;
using System.Collections.Immutable;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Extensions;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;

public sealed class ShowMainMenuCommand : SinglePanelMenuCommand
{
    private readonly ICommandFactory<MainMenuOptions> _mainMenuCommandFactory;
    private readonly Dictionary<MenuEntry, MainMenuOptions> _mainMenuOptionsMap;

    public ShowMainMenuCommand(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        ICommandFactory<MainMenuOptions> mainMenuCommandFactory
        ) : base(renderService, panelBuilderService)
    {
        _mainMenuCommandFactory = mainMenuCommandFactory;

        _mainMenuOptionsMap = MapMenuOptions();
        MenuEntries = PopulateMenuEntries();
    }
    
    protected override object GetChosenOption(ISinglePanelViewModel viewModel)
    {
        var selectedEntry = (MenuEntry)viewModel.GetCurrentElement();
        
        return _mainMenuOptionsMap[selectedEntry];
    }

    protected override ICommand GetCommand(object chosenOption) => 
        _mainMenuCommandFactory.Create((MainMenuOptions)chosenOption);

    protected override ImmutableList<IViewModelEntity> PopulateMenuEntries() =>
        _mainMenuOptionsMap.Keys.ToImmutableList<IViewModelEntity>();

    private static Dictionary<MenuEntry, MainMenuOptions> MapMenuOptions()
    {
        var mainMenuOptionsMap = new Dictionary<MenuEntry, MainMenuOptions>();
        
        foreach (var value in Enum.GetValues<MainMenuOptions>())
        {
            const int menuEntryHeight = 1;
            mainMenuOptionsMap.Add(new MenuEntry(value.GetDescription(), menuEntryHeight), value);
        }

        return mainMenuOptionsMap;
    }
}