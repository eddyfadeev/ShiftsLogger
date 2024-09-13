using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Domain.Mappers;

public class GenericReportMapper<TEntity> : JsonConverter<GenericReportModel<TEntity>>
    where TEntity : class, IReportModel
{
    public override void WriteJson(JsonWriter writer, GenericReportModel<TEntity>? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var jObject = new JObject
        {
            ["$values"] = JArray.FromObject(value.Entities, serializer)
        };

        jObject.WriteTo(writer);
    }

    public override GenericReportModel<TEntity> ReadJson(
        JsonReader reader,
        Type objectType,
        GenericReportModel<TEntity>? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);

        var entitiesToken = jObject["$values"];
        
        if (entitiesToken is not
            {
                Type: JTokenType.Array
            })
        {
            throw new JsonSerializationException("Expected JSON array under $values.");
        }

        var entities = entitiesToken.ToObject<List<TEntity>>(serializer);

        return new GenericReportModel<TEntity>
        {
            Entities = entities ?? new ()
        };
    }


}