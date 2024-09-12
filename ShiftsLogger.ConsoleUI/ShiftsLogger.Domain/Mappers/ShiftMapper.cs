using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Domain.Mappers;

public class ShiftMapper : JsonConverter<Shift>
{
    private const string ShiftIdKey = "id";
    private const string UserIdKey = "userId";
    private const string UserNameKey = "userName";
    private const string UserRoleKey = "userRole";
    private const string LocationIdKey = "locationId";
    private const string LocationNameKey = "locationName";
    private const string ShiftTypeIdKey = "shiftTypeId";
    private const string ShiftTypeDescriptionKey = "shiftTypeDescription";
    private const string StartTimeKey = "startTime";
    private const string EndTimeKey = "endTime";
    private const string HoursWorkedKey = "hoursWorked";
    private const string DescriptionKey = "description";

    public override Shift ReadJson(JsonReader reader, Type objectType, Shift? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        var shift = new Shift
        {
            Id = jObject.Value<int>(ShiftIdKey),
            UserId = jObject.Value<int>(UserIdKey),
            UserName = jObject.Value<string>(UserNameKey),
            UserRole = jObject.Value<string>(UserRoleKey),
            LocationId = jObject.Value<int>(LocationIdKey),
            LocationName = jObject.Value<string>(LocationNameKey),
            ShiftTypeId = jObject.Value<int>(ShiftTypeIdKey),
            ShiftTypeDescription = jObject.Value<string>(ShiftTypeDescriptionKey),
            StartTime = jObject.Value<DateTime>(StartTimeKey),
            EndTime = jObject.Value<DateTime>(EndTimeKey),
            HoursWorked = jObject.Value<decimal>(HoursWorkedKey),
            Description = jObject.Value<string>(DescriptionKey)
        };
        
        return shift;
    }

    public override void WriteJson(JsonWriter writer, Shift? value, JsonSerializer serializer)
    {
        JObject jObject = new JObject
        {
            { ShiftIdKey, value?.Id },
            { UserIdKey, value?.UserId },
            { UserNameKey, value?.UserName },
            { UserRoleKey, value?.UserRole },
            { LocationIdKey, value?.LocationId },
            { LocationNameKey, value?.LocationName },
            { ShiftTypeIdKey, value?.ShiftTypeId },
            { ShiftTypeDescriptionKey, value?.ShiftTypeDescription },
            { StartTimeKey, value?.StartTime },
            { EndTimeKey, value?.EndTime },
            { HoursWorkedKey, value?.HoursWorked },
            { DescriptionKey, value?.Description }
        };
        
        jObject.WriteTo(writer);
    }
}