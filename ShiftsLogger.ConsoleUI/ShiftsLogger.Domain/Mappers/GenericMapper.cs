using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Mappers;

public class GenericMapper<TEntity> : JsonConverter<GenericReportModel<TEntity>>
    where TEntity : class, IReportModel
{
    
    public override GenericReportModel<TEntity>? ReadJson(JsonReader reader, Type objectType, GenericReportModel<TEntity>? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        
        var information = jObject.ToObject<TEntity>(serializer) 
                          ?? throw new JsonSerializationException("Unable to deserialize object");
        
        var shifts = jObject["shifts"]?.ToObject<List<Shift>>(serializer) 
                     ?? new List<Shift>();

        var reportModel = new GenericReportModel<TEntity>
        {
            Information = information, 
            Shifts = shifts
        };
        
        return reportModel;
    }
    
    public override void WriteJson(JsonWriter writer, GenericReportModel<TEntity>? value, JsonSerializer serializer)
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