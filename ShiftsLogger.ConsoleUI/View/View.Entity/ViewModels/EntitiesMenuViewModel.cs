using View.Contracts;
using View.Entity.Extensions;
using View.Entity.Models;
using View.Entity.Structures;
using View.Events;

namespace View.Entity.ViewModels;

public class EntitiesMenuViewModel : MenuViewModelBase<EntityEntryData>, IPageable
{
    private int _currentPage = 1;
    
    public EntitiesMenuViewModel(
        TableSettings tableSettings, 
        params IEnumerable<EntityEntryData> menuEntries
    ) : base(tableSettings, menuEntries)
    {
        MenuData[SelectedIndex] = 
            MenuData[SelectedIndex]
                .WithStyle(MenuData.Settings.Styles.Selection);
    }

    protected override void MoveCursor(int previousIndex, int newIndex)
    {
        MenuData[previousIndex] = 
            MenuData[previousIndex]
                .WithStyle(MenuData.Settings.Styles.Content);
        
        MenuData[newIndex] = 
            MenuData[newIndex]
                .WithStyle(MenuData.Settings.Styles.Selection);
    }

    public void SubstituteData<T>(IEnumerable<T> data)
        where T : IActionable
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        var actionables = data.ToList();
        
        if (actionables.FirstOrDefault() is not EntityEntryData)
        {
            throw new ArgumentException("Invalid data type", nameof(data));
        }
        MenuData.Clear();
        MenuData.AddRange((IEnumerable<EntityEntryData>)actionables);
    }

    #region IPageable

    public event ChangePageEvent? OnPageChanged;

    public int CurrentPage
    {
        get => _currentPage;
        private set
        {
            const int firstPage = 1;
            int previousPage = _currentPage;
            
            int newPage = Math.Clamp(value, firstPage, int.MaxValue);
            if (newPage == previousPage)
            {
                return;
            }

            _currentPage = newPage;
            ResetSelection();
            OnPageChanged?.Invoke(this, new ChangePageEventArgs { CurrentPage = newPage });
        } 
    }

    // public int TotalPages
    // {
    //     get => _totalPages;
    //     set
    //     {
    //         const int minPages = 1;
    //         int previousTotalPages = _totalPages;
    //         
    //         int totalPages = Math.Clamp(value, minPages, int.MaxValue);
    //         if (totalPages == previousTotalPages)
    //         {
    //             return;
    //         }
    //         
    //         _totalPages = totalPages;
    //     }
    // }

    public void NextPage() =>
        CurrentPage++;
    

    public void PreviousPage() =>
        CurrentPage--;
    
    #endregion
}