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
    
    private int _onScreenElementsCount;
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
            return VisibleElements.ElementAt(CurrentIndex);
        }
    }

    public void MoveUp()
    {
        lock(_lock)
        {
            if (CurrentIndex != 0)
            {
                CurrentIndex--;
            }
            else if (CurrentIndex == 0 && _offset > 0)
            {
                _offset--;
                var lastElement = VisibleElements.Last;
                if (lastElement != null)
                {
                    VisibleElements.RemoveLast();
                    VisibleElements.AddFirst(_allElements[_offset]);
                }
            }
        }
    }

    public void MoveDown()
    {
        lock(_lock)
        {
            if (CurrentIndex < VisibleElements.Count - 1)
            {
                CurrentIndex++;
            }
            else if (CurrentIndex >= VisibleElements.Count - 1 && CurrentIndex + _offset < _allElements.Count - 1)
            {
                _offset++;
                var firstElement = VisibleElements.First;
                if (firstElement != null)
                {
                    VisibleElements.RemoveFirst();
                    VisibleElements.AddLast(_allElements[_offset + _onScreenElementsCount - 1]);
                }
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

    private void OnElementHeightChanged()
    {
        lock (_lock)
        {
            VisibleElements = GetVisibleElements();
        }
    }
    
    private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        lock (_lock)
        {
            AdjustForResize();
            VisibleElements = GetVisibleElements();
        }
    }

    private void OnConsoleResized()
    {
        lock(_lock)
        {
            int newOnScreenCount = CountOnScreenElements();

            if (newOnScreenCount == _onScreenElementsCount)
            {
                return;
            }
            
            _onScreenElementsCount = newOnScreenCount;
            AdjustForResize();
            VisibleElements = GetVisibleElements();
        }
    }

    private void AdjustForResize()
    {
        _offset = Math.Max(0, Math.Min(_offset, _allElements.Count - _onScreenElementsCount));
        
        if (CurrentIndex >= _onScreenElementsCount)
        {
            _offset += (CurrentIndex - _onScreenElementsCount + 1);
            CurrentIndex = _onScreenElementsCount - 1;
        }
        
        CurrentIndex = Math.Min(CurrentIndex, _onScreenElementsCount - 1);
    }
    
    private LinkedList<T> GetVisibleElements()
    {
        if (_allElements.Count == 0)
        {
            return new LinkedList<T>();
        }
        _onScreenElementsCount = CountOnScreenElements();
        
        if (_allElements.Count <= _onScreenElementsCount)
        {
            return new LinkedList<T>(_allElements);
        }
        
        _offset = Math.Max(0, Math.Min(_offset, _allElements.Count - _onScreenElementsCount));
        
        return new LinkedList<T>(_allElements.Skip(_offset).Take(_onScreenElementsCount));
    }
    
    private int CountOnScreenElements()
    {
        const int borders = 2; // sum of top and bottom border rows
        int consoleWindowHeight = Console.WindowHeight;
        int elementHeight = GetElementsHeight();

        return (consoleWindowHeight - borders) / elementHeight;
    }

    private int GetElementsHeight()
    {
        if (!_allElements.Any())
        {
            return 1;
        }
        
        var element = _allElements[0];
        return element.ElementHeight;
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
        ResizeService.Start();

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
}