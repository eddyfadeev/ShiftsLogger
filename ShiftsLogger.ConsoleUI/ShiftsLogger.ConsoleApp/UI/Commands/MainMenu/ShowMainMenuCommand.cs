using System.Collections.Frozen;
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
    private readonly FrozenDictionary<MenuEntry, MainMenuOptions> _mainMenuOptionsMap;

    public ShowMainMenuCommand(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        ICommandFactory<MainMenuOptions> mainMenuCommandFactory
        ) : base(renderService, panelBuilderService)
    {
        _mainMenuCommandFactory = mainMenuCommandFactory;

        _mainMenuOptionsMap = MapMenuOptions();
        MenuEntries = PopulateMenuEntries().ToList();
    }
    
    private protected override object GetChosenOption(ISinglePanelViewModel viewModel)
    {
        var selectedEntry = (MenuEntry)viewModel.GetCurrentElement();
        
        return _mainMenuOptionsMap[selectedEntry];
    }

    private protected override ICommand GetCommand(object chosenOption) => 
        _mainMenuCommandFactory.Create((MainMenuOptions)chosenOption);

    private protected override IEnumerable<IViewModelEntity> PopulateMenuEntries() =>
        _mainMenuOptionsMap.Keys;

    private static FrozenDictionary<MenuEntry, MainMenuOptions> MapMenuOptions()
    {
        var mainMenuOptionsMap = new Dictionary<MenuEntry, MainMenuOptions>();
        
        foreach (var value in Enum.GetValues<MainMenuOptions>())
        {
            const int menuEntryHeight = 1;
            mainMenuOptionsMap.Add(new MenuEntry(value.GetDescription(), menuEntryHeight), value);
        }

        return mainMenuOptionsMap.ToFrozenDictionary();
    }
}