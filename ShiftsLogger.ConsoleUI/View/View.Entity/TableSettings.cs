using Spectre.Console;
using View.Entity.Structures;

namespace View.Entity;

public abstract class TableSettings
{
    private int _separatorRow = 5;
    
    public bool ShowColumnHeaders { get; set; } = false;
    public bool ShowFooter { get; set; } = false;
    public bool Expand { get; set; } = false;
    public bool RowSeparatorIsActive => 
        _separatorRow > 0 ;
    public TableBorder BorderStyle { get; set; } = TableBorder.Rounded;
    
    public StringStyling? TitleStyle { get; set; } = null;
    public StringStyling? HeadersStyle { get; set; } = null;
    public StringStyling? ContentStyle { get; set; } = null;
    public StringStyling? SelectionStyle { get; set; } = null;

    public int SeparatorRow
    {
        get => _separatorRow;
        set
        {
            const int minSeparatorRow  = 0;
            const int maxSeparatorRow  = 9;

            _separatorRow = Math.Clamp(value, minSeparatorRow, maxSeparatorRow);
        }
    }
}