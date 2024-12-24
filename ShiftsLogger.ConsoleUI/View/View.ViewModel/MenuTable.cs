using View.Entity;
using View.Utility.Extensions;

namespace View.ViewModel;

public sealed class MenuTable : TableData<string>
{
    public override StyledString[] TableColumnHeaders { get; protected set; }
    public override StyledString[][] TableDataRows { get; protected set; }
    
    public MenuTable(List<string> menuEntries, TableSettings tableSettings) 
        : base(menuEntries, tableSettings)
    {
        TableColumnHeaders = [string.Empty];
        TableDataRows = GetMenuEntries(menuEntries);
    }
    
    private StyledString[][] GetMenuEntries(List<string> menuEntries)
    {
        List<string[]> preparedEntries = [];
        preparedEntries.AddRange(menuEntries.Select(entry => (string[]) [entry]));
        
        return preparedEntries.Select(
                (entry, index) =>
                    index == SelectedIndex
                        ? entry.ApplyStyle(Settings.SelectionStyle)
                        : entry.ApplyStyle(Settings.ContentStyle))
            .ToArray();
    }
}