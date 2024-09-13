using Newtonsoft.Json;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Mappers;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Models;

public class GenericReportModel<TEntity> where TEntity : class, IReportModel
{
    [JsonProperty]
    public TEntity Information { get; init; }
    
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