using Spectre.Console;

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
    
    // TODO: Extract to style struct 
    public Style? TitleStyle { get; set; } = null;
    public HorizontalAlignment TitleAlignment { get; set; } = HorizontalAlignment.Left;
    public Style? HeadersStyle { get; set; } = null;
    public HorizontalAlignment HeadersAlignment { get; set; } = HorizontalAlignment.Left;
    public Style? ContentStyle { get; set; } = null;
    public HorizontalAlignment ContentAlignment { get; set; } = HorizontalAlignment.Left;
    public Style? SelectionStyle { get; set; } = null;
    public HorizontalAlignment SelectionAlignment { get; set; } = HorizontalAlignment.Left;
    // End of extraction

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