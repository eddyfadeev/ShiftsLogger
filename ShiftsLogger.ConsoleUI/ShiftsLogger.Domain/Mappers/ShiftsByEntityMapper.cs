using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Mappers;

public class ShiftsByEntityMapper<TEntity> : JsonConverter<ShiftsByEntityReportModel<TEntity>>
    where TEntity : class, IReportModel
{
    
    public override ShiftsByEntityReportModel<TEntity> ReadJson(JsonReader reader, Type objectType, ShiftsByEntityReportModel<TEntity>? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        
        var information = jObject.ToObject<TEntity>(serializer);
        
        var shifts = jObject["shifts"]?.ToObject<List<Shift>>(serializer) 
                     ?? new List<Shift>();

        var reportModel = new ShiftsByEntityReportModel<TEntity>
        {
            Information = information ?? Activator.CreateInstance<TEntity>(), 
            Shifts = shifts
        };
        
        return reportModel;
    }
    
    public override void WriteJson(JsonWriter writer, ShiftsByEntityReportModel<TEntity>? value, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        JObject jObject = new JObject
        {
            { "information", JObject.FromObject(value.Information, serializer) },
            { "shifts", JArray.FromObject(value.Shifts, serializer) }
        };
        
        jObject.WriteTo(writer);
    }
}