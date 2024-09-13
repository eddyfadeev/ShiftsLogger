using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Mappers;

public class LocationMapper : JsonConverter<Location>
{
    private const string LocationIdKey = "id";
    private const string NameKey = "name";
    private const string AddressKey = "address";

    public override Location ReadJson(JsonReader reader, Type objectType, Location? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        var location = new Location
        {
            Id = jObject.Value<int>(LocationIdKey),
            Name = jObject.Value<string>(NameKey) ?? "No location name provided",
            Address = jObject.Value<string>(AddressKey)
        };
        
        return location;
    }
    
    public override void WriteJson(JsonWriter writer, Location? value, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        JObject jObject = new JObject
        {
            { LocationIdKey, value.Id },
            { NameKey, value.Name },
            { AddressKey, value.Address ?? string.Empty }
        };
        
        jObject.WriteTo(writer);
    }
}