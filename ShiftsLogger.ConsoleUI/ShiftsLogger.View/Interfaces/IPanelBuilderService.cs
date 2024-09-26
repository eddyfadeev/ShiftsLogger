using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IPanelBuilderService
{
    Panel PrepareRenderablePanel<TPanelEntries>(SinglePanelViewModel<TPanelEntries> renderInfo)
        where TPanelEntries : class;

    Tuple<Panel, Panel> PrepareRenderablePanels<TLeftPanelEntries, TRightPanelEntries>(
        DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> renderInfo)
        where TLeftPanelEntries : class
        where TRightPanelEntries : class;
}