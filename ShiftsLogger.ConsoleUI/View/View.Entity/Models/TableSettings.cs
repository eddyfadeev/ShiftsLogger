namespace View.Entity.Models;

public abstract class TableSettings
{
    private int _separatorRow = 5;
    
    public bool ShowFooter { get; set; } = false;
    public bool ShowHeaders { get; set; }
    public bool Expand { get; set; } = false;
    public bool RowSeparatorIsActive => 
        _separatorRow > 0 ;
    
    public TableMenuStyles Styles { get; set; } = new ();

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