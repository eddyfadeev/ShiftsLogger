using Spectre.Console;

namespace View.Entity;

public class RenderData<T> : List<T>
{
    public RenderData(RenderSettings renderSettings, params IEnumerable<T> renderableObjects) 
    {
        Settings = renderSettings 
                   ?? throw new ArgumentNullException(nameof(renderSettings), "Table settings cannot be null");
        
        AddRange(renderableObjects);
    }

    public Text Title { get; set; } = new (string.Empty);
    public Text Footer { get; set; } = new (string.Empty);
    public RenderSettings Settings { get; set; }
}