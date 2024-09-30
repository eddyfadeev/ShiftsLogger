using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.ViewModels;

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
                _viewModel.MoveUp();
                break;
            case Enums.Selection.MoveDown:
                _viewModel.MoveDown();
                break;
            case Enums.Selection.Select:
                Console.WriteLine($"You selected: {_viewModel.GetCurrentChoice()}");
                Environment.Exit(0);
                break;
        }
    }
}