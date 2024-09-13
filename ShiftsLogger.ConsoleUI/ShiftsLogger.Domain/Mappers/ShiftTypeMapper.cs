using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Mappers;

public class ShiftTypeMapper : JsonConverter<ShiftType>
{
    private const string ShiftTypeIdKey = "id";
    private const string NameKey = "name";

    public override ShiftType? ReadJson(JsonReader reader, Type objectType, ShiftType? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        var shiftType = new ShiftType
        {
            Id = jObject.Value<int>(ShiftTypeIdKey),
            Name = jObject.Value<string>(NameKey) ?? "No shift type name provided"
        };
        
        return shiftType;
    }
    
    public override void WriteJson(JsonWriter writer, ShiftType? value, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        JObject jObject = new JObject()
        {
            { ShiftTypeIdKey, value.Id },
            { NameKey, value.Name }
        };
        
        jObject.WriteTo(writer);
    }
}