using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Extensions;
using Terminal.Gui;

namespace ShiftsLogger.ConsoleApp.ConsoleUI;

public interface IMenu
{
    void Display();
}

public class MainMenuWindow : Window, IMenu
{
    public MainMenuWindow()
    {
        Title = "Main Menu";
        BorderStyle = LineStyle.Rounded;
        Display();
    }

    public void Display()
    {
        var mainMenu = CreateMainMenu();
        Add(mainMenu);
    }

    private FrameView CreateMainMenu()
    {
        var menu = ConfigureFrameView();
        
        var buttons = CreateMenuButtons();
        buttons.ForEach(b => menu.Add(b));
        
        return menu;
    }

    private FrameView ConfigureFrameView() =>
        new ()
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Border =
            {
                Thickness = new Thickness()
            }
        };
    
    private static List<Button> CreateMenuButtons()
    {
        List<Button> menuButtons = new();
        IEnumerable<string> menuEntries = GetMenuEntries();

        int buttonY = 0;
        const int verticalSpacing = 2;

        foreach (var menuEntry in menuEntries)
        {
            var button = CreateButton(menuEntry, buttonY);
            button.Accept += (s,e) => OpenNewWindow(menuEntry);
            menuButtons.Add(button);
            
            buttonY += verticalSpacing;
        }
        
        return menuButtons;
    }

    private static IEnumerable<string> GetMenuEntries() =>
        Enum.GetValues(typeof(MainMenuOptions))
            .Cast<Enum>()
            .Select(e => e.GetDescription());

    private static Button CreateButton(string buttonName, int buttonYPosition) =>
        new()
        {
            Text = buttonName,
            X = Pos.Center(),
            Y = buttonYPosition,
        };

    private static void OpenNewWindow(string windowTitle)
    {
        var newWindow = new Window()
        {
            Title = windowTitle,
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Border = { BorderStyle = LineStyle.Rounded }
        };
        
        var backButton = new Button
        {
            Title = "Back",
            X = Pos.Center(),
            Y = Pos.Center(),
        };

        backButton.Accept += (s, e) => Terminal.Gui.Application.RequestStop();
        newWindow.Add(backButton);
        
        Terminal.Gui.Application.Run(newWindow);
    }
}