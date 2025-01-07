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
        
        TotalPages = ((EntitiesTableSettings)tableSettings).TotalPages;
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
        var entityEntryData = data.ToList();
        if (TotalPages != entityEntryData.Count)
        {
            throw new ArgumentException("Data count mismatch", nameof(data));
        }
        
        MenuData.Clear();
        MenuData.AddRange(entityEntryData);
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

    public void NextPage(params IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage++;
    }

    public void PreviousPage(params IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage--;
    }

    public void SetCurrentPage(int page, params IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage = page;
    }

    public void ResetPage(params IEnumerable<EntityEntryData> items)
    {
        SubstituteData(items);
        CurrentPage = 1;
    }

    #endregion
}