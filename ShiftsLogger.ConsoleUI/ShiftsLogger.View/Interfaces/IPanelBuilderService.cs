using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IPanelBuilderService
{
    Panel CreatePanel(
        ISinglePanelViewModel viewModel,
        HorizontalAlignment textAlignment,
        Color color,
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
    );

    Panel CreateDummyPanel(string text);
}