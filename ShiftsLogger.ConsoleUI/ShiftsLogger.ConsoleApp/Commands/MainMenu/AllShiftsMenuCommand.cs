using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.Services;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public sealed class AllShiftsMenuCommand : SinglePanelMenuCommand<Shift>
{
    private readonly ShiftsController _shiftsController;

    public AllShiftsMenuCommand(IRenderService renderService, IPanelBuilderService panelBuilderService, ShiftsController shiftsController) 
        : base(renderService, panelBuilderService)
    {
        _shiftsController = shiftsController;
    }

    private protected override object GetChosenOption(ISinglePanelViewModel<Shift> viewModel) =>
        viewModel.GetCurrentElement();

    private protected override ICommand GetCommand(object chosenOption)
    {
        throw new NotImplementedException();
    }

    private protected override void ProcessUserInput(ISelectionService selectionService, ISinglePanelViewModel<Shift> viewModel)
    {
        var key = Console.ReadKey(true).Key;
        
        switch (key)
        {
            case ConsoleKey.UpArrow:
                selectionService.ChangeSelection(Selection.MoveUp);
                break;
            case ConsoleKey.DownArrow:
                selectionService.ChangeSelection(Selection.MoveDown);
                break;
            case ConsoleKey.Enter:
                var chosenOption = GetChosenOption(viewModel);
                Console.WriteLine(chosenOption.ToString());
                Console.WriteLine("Any key to continue...");
                Console.ReadKey();
                IsMenuRunning = false;
                break;
            case ConsoleKey.Escape:
                IsMenuRunning = false;
                break;
        }
    }
    
    private protected override List<Shift> PopulateMenuEntries() =>
        _shiftsController.GetAllShifts().Result;
}