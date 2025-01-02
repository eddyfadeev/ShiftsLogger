using Spectre.Console;

namespace View.Entity;

public abstract class RenderSettings
{
    private int _separatorRow = 5;
    
    public bool ShowFooter { get; set; } = false;
    public bool Expand { get; set; } = false;
    public bool RowSeparatorIsActive => 
        _separatorRow > 0 ;
    public TableBorder BorderStyle { get; set; } = TableBorder.Rounded;
    
    public Style? TitleStyle { get; set; } = null;
    public Style? FooterStyle { get; set; } = null;
    public Style? ContentStyle { get; set; } = null;
    public Style? SelectionStyle { get; set; } = null;

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