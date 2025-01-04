using Spectre.Console;

namespace View.Entity;

public class TableData<T> : List<T>
{
    public TableData(TableSettings tableSettings, params IEnumerable<T> tableObjects) 
    {
        Settings = tableSettings 
                   ?? throw new ArgumentNullException(nameof(tableSettings), "Table settings cannot be null");
        
        AddRange(tableObjects);
    }

    public TableTitle Title { get; set; } = new (string.Empty);
    public TableTitle Footer { get; set; } = new (string.Empty);
    public TableSettings Settings { get; set; }
}