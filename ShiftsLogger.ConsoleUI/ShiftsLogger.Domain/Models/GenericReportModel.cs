using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Domain.Models;

public class GenericReportModel<TEntity> : IReportModel
    where TEntity : class, IReportModel
{
    public List<TEntity> Entities { get; init; } = new();

    public TEntity this[int index]
    {
        get
        {
            if (index < 0 || index >= Entities.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    index,
                    $"Index out of range. Index must be between 0 and {Entities.Count - 1} (inclusive)!");
            }
            
            return Entities[index];
        }
    }

    public int Id { get; init; }
}