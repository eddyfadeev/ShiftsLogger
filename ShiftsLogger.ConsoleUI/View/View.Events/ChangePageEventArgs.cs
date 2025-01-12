namespace View.Events;

public class ChangePageEventArgs : EventArgs
{
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
    public int? TotalPages { get; set; }
}