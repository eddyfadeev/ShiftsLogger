using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models.Entities;

namespace Shared.Models;

public class GenericReportModel<TEntity> where TEntity : class, IReportModel
{
    public TEntity Information { get; set; }
    
    [JsonProperty("shifts")] public List<Shift> Shifts { get; set; } = new();

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