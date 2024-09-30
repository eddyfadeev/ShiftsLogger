using System.Collections.Frozen;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Extensions;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public sealed class ShowMainMenuCommand : SinglePanelMenuCommand<string>
{
    private readonly ICommandFactory<MainMenuOptions> _mainMenuCommandFactory;
    private readonly FrozenDictionary<string, MainMenuOptions> _mainMenuOptionsMap;

    public ShowMainMenuCommand(
        IRenderService renderService, 
        IPanelBuilder panelBuilder, 
        ICommandFactory<MainMenuOptions> mainMenuCommandFactory
        ) : base(renderService, panelBuilder)
    {
        _mainMenuCommandFactory = mainMenuCommandFactory;

        MenuEntries = PopulateMenuEntries();
        _mainMenuOptionsMap = MapMenuOptions();
    }
    
    private protected override object GetChosenOption(SinglePanelViewModel<string> viewModel)
    {
        string selectedEntry = viewModel.GetCurrentChoice();
        
        return _mainMenuOptionsMap[selectedEntry];
    }

    private protected override ICommand GetCommand(object chosenOption) => 
        _mainMenuCommandFactory.Create((MainMenuOptions)chosenOption);

    private protected override List<string> PopulateMenuEntries() =>
        EnumExtensions.GetDescriptions<MainMenuOptions>().ToList();
    
    private static FrozenDictionary<string, MainMenuOptions> MapMenuOptions()
    {
        var mainMenuOptionsMap = new Dictionary<string, MainMenuOptions>();
        
        foreach (var value in Enum.GetValues<MainMenuOptions>())
        {
            mainMenuOptionsMap.Add(value.GetDescription(), value);
        }

        return mainMenuOptionsMap.ToFrozenDictionary();
    }
}