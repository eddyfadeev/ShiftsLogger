using Spectre.Console;
using View.Contracts;

namespace View.Entity.Structures;

public readonly record struct EntityEntryData : IActionable
{
    public EntityEntryData(
        Text[] headers, 
        Text[] entryData,
        Action? entryAction = null)
    {
        Headers = headers; 
        EntryData = entryData;   
        Action = entryAction;
    }
    
    public Text[] Headers { get; init; }
    public Text[] EntryData { get; init; }
    public Action? Action { get; init; }
}