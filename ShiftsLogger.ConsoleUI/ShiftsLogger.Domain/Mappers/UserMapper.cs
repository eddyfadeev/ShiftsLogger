using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Mappers;

public class UserMapper : JsonConverter<User>
{
    private const string IdKey = "id";
    private const string FirstNameKey = "firstName";
    private const string LastNameKey = "lastName";
    private const string EmailKey = "email";
    private const string RoleKey = "role";
    
    public override User ReadJson(JsonReader reader, Type objectType, User? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        var user = new User
        {
            Id = jObject.Value<int>(IdKey),
            FirstName = jObject.Value<string>(FirstNameKey) ?? "No first name provided",
            LastName = jObject.Value<string>(LastNameKey),
            Email = jObject.Value<string>(EmailKey) ?? "No email provided",
            Role = jObject.Value<string>(RoleKey)
        };
        
        return user;
    }
    
    public override void WriteJson(JsonWriter writer, User? value, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        JObject jObject = new JObject
        {
            { IdKey, value.Id },
            { FirstNameKey, value.FirstName },
            { LastNameKey, value.LastName ?? string.Empty },
            { EmailKey, value.Email },
            { RoleKey, value.Role ?? string.Empty }
        };
        
        jObject.WriteTo(writer);
    }
}