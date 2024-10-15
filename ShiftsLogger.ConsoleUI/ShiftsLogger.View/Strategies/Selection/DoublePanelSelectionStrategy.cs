using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.Strategies;
using ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

namespace ShiftsLogger.View.Strategies.Selection;

public class DoublePanelSelectionStrategy : ISelectionStrategy
{
    private readonly IDoublePanelViewModel _viewModel;

    public DoublePanelSelectionStrategy(IDoublePanelViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public void ChangeSelection(Enums.Selection move)
    {
        switch (move)
        {
            case Enums.Selection.MoveUp 
                when _viewModel.IsSinglePanelMode:
                _viewModel.LeftPanelViewModel.SelectPrevious();
                break;
            case Enums.Selection.MoveUp 
                when !_viewModel.IsSinglePanelMode:
                _viewModel.RightPanelViewModel.SelectPrevious();
                break;
            case Enums.Selection.MoveDown 
                when _viewModel.IsSinglePanelMode:
                _viewModel.LeftPanelViewModel.SelectNext();
                break;
            case Enums.Selection.MoveDown 
                when !_viewModel.IsSinglePanelMode:
                _viewModel.RightPanelViewModel.SelectNext();
                break;
            case Enums.Selection.MoveLeft 
                when _viewModel.IsFilterSelected:
                _viewModel.SelectLeftPanel();
                break;
            case Enums.Selection.MoveRight 
                when _viewModel is { IsFilterSelected: true, RightPanelViewModel.PanelEntries.VisibleElements.Count: > 0 }:
                _viewModel.SelectRightPanel();
                break;
            case Enums.Selection.Select 
                when _viewModel.IsSinglePanelMode:
                _viewModel.ActivateFilter();
                _viewModel.ResetRightPanelSelection();
                break;
            case Enums.Selection.Select 
                when !_viewModel.IsSinglePanelMode:
                Console.WriteLine($"You selected: {_viewModel.LeftPanelViewModel.GetCurrentElement()}, " +
                                  $"{_viewModel.RightPanelViewModel.GetCurrentElement()}");
                Environment.Exit(0);
                break;
        }
    }
}