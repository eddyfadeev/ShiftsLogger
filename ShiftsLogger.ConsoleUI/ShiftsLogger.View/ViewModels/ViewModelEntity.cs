using ShiftsLogger.View.Interfaces.ViewModels;

namespace ShiftsLogger.View.ViewModels;

public abstract class ViewModelEntity : IViewModelEntity
{
    private int _elementHeight = 1;
    
    public event Action? ElementHeightChanged;

    public int ElementHeight
    {
        get => _elementHeight;
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException("Height cannot be less than zero!");
            }

            if (_elementHeight != value)
            {
                _elementHeight = value;
                ElementHeightChanged?.Invoke();
            }
        }
    }

    public void SetElementHeight(int height)
    {
        ElementHeight = height;
    }
    
    public abstract string GetShortRepresentation();

    public abstract string GetDetailedRepresentation();
}