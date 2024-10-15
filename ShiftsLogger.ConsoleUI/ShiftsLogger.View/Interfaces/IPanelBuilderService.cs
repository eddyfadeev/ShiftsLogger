using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IPanelBuilderService
{
    Panel CreatePanel(
        Func<IViewModelEntity, string> menuRepresentation,
        ISinglePanelViewModel viewModel,
        HorizontalAlignment textAlignment,
        Color selectorColor,
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
    );

    Panel CreateDummyPanel(string text);
}