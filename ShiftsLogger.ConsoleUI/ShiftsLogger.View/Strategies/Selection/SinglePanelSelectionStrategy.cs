using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.View.Strategies.Selection;

public class SinglePanelSelectionStrategy : ISelectionStrategy
{
    private readonly ISinglePanelViewModel _viewModel;

    public SinglePanelSelectionStrategy(ISinglePanelViewModel viewModel)
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
                Console.WriteLine($"You selected: {_viewModel.GetCurrentElement()}");
                Environment.Exit(0);
                break;
        }
    }
}