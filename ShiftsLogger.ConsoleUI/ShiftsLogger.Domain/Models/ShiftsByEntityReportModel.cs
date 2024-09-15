
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Models;

public class ShiftsByEntityReportModel<TEntity> : IReportModel
    where TEntity : class, IReportModel
{
    public TEntity Information { get; init; }
    
    public List<Shift> Shifts { get; init; } = new();

    public Shift this[int index]
    {
        get
        {
            if (index < 0 || index >= Shifts.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    index,
                    $"Index out of range. Index must be between 0 and {Shifts.Count - 1} (inclusive)!");
            }
            
            return Shifts[index];
        }
    }
}