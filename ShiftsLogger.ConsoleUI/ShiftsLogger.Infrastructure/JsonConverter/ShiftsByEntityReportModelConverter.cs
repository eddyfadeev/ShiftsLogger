using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Infrastructure.JsonConverter;

public class ShiftsByEntityReportModelConverter<TEntity> : Newtonsoft.Json.JsonConverter
    where TEntity : class, IReportModel
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        
        var model = new ShiftsByEntityReportModel<TEntity>
        {
            Information = jsonObject.ToObject<TEntity>(serializer)
        };

        var shifts = jsonObject["shifts"]["$values"];
        foreach (var shift in shifts)
        {
            model.Shifts.Add(shift.ToObject<Shift>(serializer));
        }

        return model;
    }

    public override bool CanConvert(Type objectType) =>
        objectType == typeof(ShiftsByEntityReportModel<TEntity>);
}