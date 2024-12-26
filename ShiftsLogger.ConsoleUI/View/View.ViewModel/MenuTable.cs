using View.Entity;
using View.Entity.Structures;
using View.Utility.Extensions;

namespace View.ViewModel;

public sealed class MenuTable : TableData<StyledString>
{
    public override StyledString[] TableColumnHeaders { get; protected set; }
    public override StyledString[][] TableDataRows { get; protected set; }
    
    public MenuTable(List<StyledString> menuEntries, TableSettings tableSettings) 
        : base(menuEntries, tableSettings)
    {
        TableColumnHeaders = [new StyledString()];
        TableDataRows = GetMenuEntries(menuEntries);
    }
    
    private StyledString[][] GetMenuEntries(List<StyledString> menuEntries)
    {
        List<StyledString[]> preparedEntries = [];
        preparedEntries.AddRange(menuEntries.Select(entry => (StyledString[]) [entry]));
        
        return preparedEntries.Select(
                (entry, index) =>
                    index == SelectedIndex
                        ? entry.ApplyStyle(Settings.SelectionStyle)
                        : entry.ApplyStyle(Settings.ContentStyle))
            .ToArray();
    }
}