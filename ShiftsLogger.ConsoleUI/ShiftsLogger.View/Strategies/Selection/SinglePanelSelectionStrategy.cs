using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.View.Strategies.Selection;

public class SinglePanelSelectionStrategy<TEntry> : ISelectionStrategy
    where TEntry : class
{
    private readonly SinglePanelViewModel<TEntry> _viewModel;

    public SinglePanelSelectionStrategy(SinglePanelViewModel<TEntry> viewModel)
    {
        _viewModel = viewModel;
    }

    public void ChangeSelection(Enums.Selection move)
    {
        switch (move)
        {
            case Enums.Selection.MoveUp:
                _viewModel.UpdateSelectionIndex(_viewModel.SelectedEntryIndex - 1);
                break;
            case Enums.Selection.MoveDown:
                _viewModel.UpdateSelectionIndex(_viewModel.SelectedEntryIndex + 1);
                break;
            case Enums.Selection.Select:
                Console.WriteLine($"You selected: {_viewModel.PanelEntries[_viewModel.SelectedEntryIndex]}");
                Environment.Exit(0);
                break;
        }
    }
}