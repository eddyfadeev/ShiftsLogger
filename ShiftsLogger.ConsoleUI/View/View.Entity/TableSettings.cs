using Spectre.Console;

namespace View.Entity;

public class TableSettings
{
    private int _separatorRow = 5;
    
    public bool ShowFooter { get; set; } = false;
    public TableBorder Border { get; set; } = TableBorder.Rounded;
    public bool Expand { get; set; } = false;
    public bool ShowTitle { get; set; } = false;

    public int SeparatorRow
    {
        get => _separatorRow;
        set
        {
            const int minSeparatorRow  = 1;
            const int maxSeparatorRow  = 10;
            const int defaultSeparatorRow = 5;
            
            _separatorRow = value is 
                > minSeparatorRow and < maxSeparatorRow 
                ? value : defaultSeparatorRow;
        }
    }
}