using ShiftsLogger.View.Enums;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IPanelBuilderService
{
    Panel CreatePanel(
        SinglePanelViewModel viewModel,
        HorizontalAlignment textAlignment,
        Color color,
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
    );

    Panel CreateDummyPanel(string text);
}