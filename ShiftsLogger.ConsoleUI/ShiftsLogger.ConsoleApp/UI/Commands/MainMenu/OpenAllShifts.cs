using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.ConsoleApp.UI.Extensions;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.LayoutComposition;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;

public sealed class OpenAllShifts : DoublePanelMenuCommand
{
    private readonly ShiftsController _shiftsController;

    public OpenAllShifts(IRenderService renderService, IPanelBuilderService panelBuilderService, ShiftsController shiftsController) 
        : base(renderService, panelBuilderService)
    {
        _shiftsController = shiftsController;
    }

    protected override ISinglePanelViewModel CreateSecondaryViewModel() =>
        new DetailedViewVIewModel(RightPanelEntries.First());

    protected override IEnumerable<ShiftViewEntity> FetchLeftPanelData() =>
        _shiftsController.GetAllShifts().Result.Select(s => s.MapToViewEntity());

    protected override IEnumerable<ShiftViewEntity> FetchRightPanelData(int entityId) =>
        [_shiftsController.GetShiftById(entityId).Result.MapToViewEntity()];

    protected override IRenderable GetRightRenderable(IDoublePanelViewModel viewModel)
    {
        return viewModel switch
        {
            { SelectedFilterIndex: < 0 } =>
                PanelBuilderService.CreateDummyPanel(
                    "[grey]Choose a shift to see details...[/]"),
            _ =>
                CreateDetailedInfoTable(viewModel)
        };
    }

    private Table CreateDetailedInfoTable(IDoublePanelViewModel viewModel)
    {
        var bottom = PanelBuilderService.CreatePanel(
            menuRepresentation: menuEntry => menuEntry.GetDetailedRepresentation(),
            viewModel: viewModel.RightPanelViewModel,
            textAlignment: HorizontalAlignment.Center,
            selectorColor: Color.Blue,
            isSinglePanel: !viewModel.IsSinglePanelMode,
            TextDecorations.Bold, TextDecorations.Underline
        ); // These are choosable entries
        
        bottom.Border(BoxBorder.None);
        bottom.Expand = false;

        var table = new Table();
        table.AddColumn(new TableColumn(""));
        table.AddRow((viewModel.RightPanelViewModel as DetailedViewVIewModel).TopPanel);
        table.AddRow(bottom);
        table.Expand = true;
        table.HideHeaders();
        table.HideFooters();
        table.Border = TableBorder.Rounded;
        
        AnsiConsole.Write(table);

        return table;
    }
}