using System.Collections.ObjectModel;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.Services;

namespace ShiftsLogger.View.ViewModels;

public class OnScreenMenuList<T> : IReturnsEntry<T>, IScrollable, IDisposable
    where T : IViewModelEntity
{
    private readonly ObservableCollection<T> _allElements;
    private readonly object _lock = new();
    
    private int _offset;
    private bool _disposed = false;

    public LinkedList<T> VisibleElements { get; private set; }
    public int CurrentIndex { get; private set; }
    
    public OnScreenMenuList(IEnumerable<T> menuEntries)
    {
        _allElements = new ObservableCollection<T>(menuEntries);
        VisibleElements = GetVisibleElements();
        SubscribeToEvents();
    }

    public T GetCurrentElement()
    {
        lock (_lock)
        {
            if (CurrentIndex < 0 || CurrentIndex >= VisibleElements.Count)
            {
                throw new InvalidOperationException(
                    $"CurrentIndex: '{CurrentIndex}' is out of range. " +
                    $"Number of elements in VisibleElements: {VisibleElements.Count}." +
                    $"Number of elements in _allElements: {_allElements.Count}");
            }
            
            return VisibleElements.ElementAt(CurrentIndex);
        }
    }

    public void SelectPrevious()
    {
        lock(_lock)
        {
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
            }
            else if (_offset > 0)
            {
                _offset--;
                UpdateVisibleElementsOnScroll();
            }
        }
    }

    public void SelectNext()
    {
        lock(_lock)
        {
            if (CurrentIndex < VisibleElements.Count - 1)
            {
                CurrentIndex++;
            }
            else if (CurrentIndex + _offset < _allElements.Count - 1)
            {
                _offset++;
                UpdateVisibleElementsOnScroll();
            }
        }
    }

    public void ResetSelection()
    {
        lock(_lock)
        {
            CurrentIndex = 0;
            _offset = 0;
            VisibleElements = GetVisibleElements();
        }
    }
    
    private void UpdateVisibleElementsOnScroll() =>
        VisibleElements = GetVisibleElements();
    
    private LinkedList<T> GetVisibleElements()
    {
        lock (_lock)
        {
            int availableHeight = GetAvailableHeight();
            var visibleElements = GetHeightAdjustedElements(availableHeight).ToList();
            
            _offset = Math.Max(0, Math.Min(_offset, _allElements.Count - visibleElements.Count));
            
            return new LinkedList<T>(visibleElements);
        }
    }
    
    private IEnumerable<T> GetHeightAdjustedElements(int availableHeight)
    {
        var elements = _allElements.Skip(_offset);
        int height = 0;

        foreach (var element in elements)
        {
            if (height + element.ElementHeight > availableHeight)
            {
                yield break;
            }

            yield return element;
            height += element.ElementHeight;
        }
    }
    
    private static int GetAvailableHeight()
    {
        const int borders = 2; // sum of top and bottom border rows
        return Console.WindowHeight - borders;
    }

    private void SubscribeToEvents()
    {
        _allElements.CollectionChanged += OnCollectionChanged;
        ResizeService.ConsoleResized += OnConsoleResized;
        ResizeService.Start();

        foreach (var entity in _allElements)
        {
            entity.ElementHeightChanged += OnElementHeightChanged;
        }
    }

    private void UnsubscribeFromEvents()
    {
        _allElements.CollectionChanged -= OnCollectionChanged;
        ResizeService.ConsoleResized -= OnConsoleResized;
        ResizeService.Stop();

        foreach (var entity in _allElements)
        {
            entity.ElementHeightChanged -= OnElementHeightChanged;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                UnsubscribeFromEvents();
            }

            _disposed = true;
        }
    }

    ~OnScreenMenuList()
    {
        Dispose(false);
    }

    private void OnConsoleResized() =>
        VisibleElements = GetVisibleElements();

    private void OnElementHeightChanged() =>
        VisibleElements = GetVisibleElements();

    private void OnCollectionChanged(object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
        VisibleElements = GetVisibleElements();
}