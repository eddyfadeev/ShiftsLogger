using Spectre.Console;

namespace View.Entity.Models;

public class MenuStyles
{
    public Style Title { get; set; } = new (foreground: Color.Olive);
    public Style Footer { get; set; } = new(foreground: Color.White);
    public Style Content { get; set; } = new(foreground: Color.White);
    public Style Selection { get; set; } = new(foreground: Color.Gold3_1);
}