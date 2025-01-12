namespace View.Contracts;

public interface IMenu
{
    public ISelectable GetViewModel();
    void DisplayMenu();
}