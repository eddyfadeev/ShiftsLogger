using ShiftsLogger.View.Enums;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IPanelBuilderService
{
    Panel CreatePanel<TPanelEntries>(
        SinglePanelViewModel<TPanelEntries> renderInfo,
        HorizontalAlignment textAlignment,
        Color color,
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
    )
        where TPanelEntries : class;

    Panel CreateDummyPanel(string text);
}