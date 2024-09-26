using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.View.Strategies.Selection;

public class DoublePanelSelectionStrategy<TLeftPanelEntries, TRightPanelEntries> : ISelectionStrategy
    where TLeftPanelEntries : class
    where TRightPanelEntries : class
{
    private readonly DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> _viewModel;

    public DoublePanelSelectionStrategy(DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> viewModel)
    {
        _viewModel = viewModel;
    }

    public void ChangeSelection(Enums.Selection move)
    {
        switch (move)
        {
            case Enums.Selection.MoveUp 
                when _viewModel.IsSinglePanelMode:
                _viewModel.UpdateLeftPanelIndex(_viewModel.SelectedEntryIndex - 1);
                break;
            case Enums.Selection.MoveUp 
                when !_viewModel.IsSinglePanelMode:
                _viewModel.UpdateRightPanelIndex(_viewModel.RightPanelActiveIndex - 1);
                break;
            case Enums.Selection.MoveDown 
                when _viewModel.IsSinglePanelMode:
                _viewModel.UpdateLeftPanelIndex(_viewModel.SelectedEntryIndex + 1);
                break;
            case Enums.Selection.MoveDown 
                when !_viewModel.IsSinglePanelMode:
                _viewModel.UpdateRightPanelIndex(_viewModel.RightPanelActiveIndex + 1);
                break;
            case Enums.Selection.MoveLeft 
                when _viewModel.IsFilterSelected:
                _viewModel.SelectLeftPanel();
                break;
            case Enums.Selection.MoveRight 
                when _viewModel.IsFilterSelected:
                _viewModel.SelectRightPanel();
                break;
            case Enums.Selection.Select 
                when _viewModel.IsSinglePanelMode:
                _viewModel.ActivateFilter();
                _viewModel.SelectRightPanel();
                _viewModel.ResetRightPanelSelection();
                break;
            case Enums.Selection.Select 
                when !_viewModel.IsSinglePanelMode:
                Console.WriteLine($"You selected: {_viewModel.PanelEntries[_viewModel.LastActiveSelectionIndex]}, " +
                                  $"{_viewModel.RightPanelEntries[_viewModel.PanelEntries[_viewModel.LastActiveSelectionIndex]][_viewModel.RightPanelActiveIndex]}");
                Environment.Exit(0);
                break;
        }
    }
}