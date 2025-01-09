using View.Contracts;
using View.Entity.Extensions;
using View.Entity.Models;
using View.Entity.Structures;

namespace View.Entity.ViewModels;

public class EntitiesMenuViewModel : MenuViewModelBase<EntityEntryData>, IPageable<EntityEntryData>
{
    private int _currentPage = 1;
    private int _totalPages = 1;
    
    public EntitiesMenuViewModel(
        TableSettings tableSettings, 
        params IEnumerable<EntityEntryData> menuEntries
    ) : base(tableSettings, menuEntries)
    {
        MenuData[SelectedIndex] = 
            MenuData[SelectedIndex]
                .WithStyle(MenuData.Settings.SelectionStyle);
    }

    protected override void MoveCursor(int previousIndex, int newIndex)
    {
        MenuData[previousIndex] = 
            MenuData[previousIndex]
                .WithStyle(MenuData.Settings.ContentStyle);
        
        MenuData[newIndex] = 
            MenuData[newIndex]
                .WithStyle(MenuData.Settings.SelectionStyle);
    }

    private void SubstituteData(IEnumerable<EntityEntryData> data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        MenuData.Clear();
        MenuData.AddRange(data);
    }

    #region IPageable
    
    public int CurrentPage
    {
        get => _currentPage;
        private set
        {
            const int firstPage = 1;
            int previousPage = _currentPage;
            
            int newPage = Math.Clamp(value, firstPage, TotalPages);
            if (newPage == previousPage)
            {
                return;
            }

            _currentPage = newPage;
            ResetSelection();
        } 
    }

    public int TotalPages
    {
        get => _totalPages;
        set
        {
            const int minPages = 1;
            int previousTotalPages = _totalPages;
            
            int totalPages = Math.Clamp(value, minPages, int.MaxValue);
            if (totalPages == previousTotalPages)
            {
                return;
            }
            
            _totalPages = totalPages;
        }
    }

    public void NextPage(IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage++;
    }

    public void PreviousPage(IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage--;
    }

    public void SetCurrentPage(IEnumerable<EntityEntryData> items, int page)
    {
        SubstituteData(items);
        CurrentPage = page;
    }

    public void ResetPage(IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage = 1;
    }
    
    #endregion
}