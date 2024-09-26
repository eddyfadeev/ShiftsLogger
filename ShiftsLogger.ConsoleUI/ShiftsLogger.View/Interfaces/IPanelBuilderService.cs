using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IPanelBuilderService
{
    Panel PrepareRenderablePanel<TPanelEntries>(
        SinglePanelViewModel<TPanelEntries> renderInfo, HorizontalAlignment textAlignment
    )
        where TPanelEntries : class;

    Tuple<Panel, Panel> PrepareRenderablePanels<TLeftPanelEntries, TRightPanelEntries>(
        DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> renderInfo,
        HorizontalAlignment leftPanelTextAlignment,
        HorizontalAlignment rightPanelTextAlignment)
        where TLeftPanelEntries : class
        where TRightPanelEntries : class;
}