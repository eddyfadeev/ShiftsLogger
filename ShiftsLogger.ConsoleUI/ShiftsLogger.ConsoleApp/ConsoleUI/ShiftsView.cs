using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.ConsoleUI;

public class ShiftsView<TEntity>
    where TEntity : class, IReportModel
{
    public int SelectedEntityIndex { get; private set; }
    public int SelectedShiftIndex { get; private set; }
    public int LastActiveSelectionIndex { get; private set; }
    public bool IsLeftPaneActive { get; private set; }
    private bool IsFilterSelected { get; set; }

    public List<TEntity> Entities { get; init; }
    public Dictionary<TEntity, List<Shift>> FilteredShifts { get; init; }

    public ShiftsView(List<TEntity> entities, Dictionary<TEntity, List<Shift>> filteredShifts)
    {
        Entities = entities;
        FilteredShifts = filteredShifts;
        LastActiveSelectionIndex = -1;
        IsLeftPaneActive = true;
    }

    public void ChangeSelection(Selection move)
    {
        var shiftsLength = IsFilterSelected ? FilteredShifts[Entities[LastActiveSelectionIndex]].Count : 0;
        
        switch (move)
        {
            case Selection.MoveUp 
                when IsLeftPaneActive && SelectedEntityIndex > 0:
                SelectedEntityIndex--;
                break;
            case Selection.MoveUp 
                when !IsLeftPaneActive && SelectedShiftIndex > 0:
                SelectedShiftIndex--;
                break;
            case Selection.MoveDown
                when IsLeftPaneActive && SelectedEntityIndex < Entities.Count - 1:
                SelectedEntityIndex++;
                break;
            case Selection.MoveDown
                when !IsLeftPaneActive && SelectedShiftIndex < shiftsLength - 1:
                SelectedShiftIndex++;
                break;
            case Selection.MoveLeft when IsFilterSelected:
                SelectLeftPane();
                break;
            case Selection.MoveRight when IsFilterSelected:
                SelectRightPane();
                break;
            case Selection.Select
                when IsLeftPaneActive:
                IsFilterSelected = true;
                SelectRightPane();
                LastActiveSelectionIndex = SelectedEntityIndex;
                SelectedShiftIndex = 0;
                break;
            case Selection.Select
                when !IsLeftPaneActive:
                AnsiConsole.MarkupLine($"[bold green] You selected : {Entities[LastActiveSelectionIndex]}, \n {FilteredShifts[Entities[LastActiveSelectionIndex]][SelectedShiftIndex]}[/]");
                Environment.Exit(0);
                break;
        }
    }

    public void SelectLeftPane() => IsLeftPaneActive = true;
    
    public void SelectRightPane() => IsLeftPaneActive = false;
}