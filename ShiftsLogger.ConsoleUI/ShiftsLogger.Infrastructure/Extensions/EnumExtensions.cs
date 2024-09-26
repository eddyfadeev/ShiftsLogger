using System.ComponentModel;
using System.Reflection;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fieldName = value.ToString();
        
        var field = value.GetType().GetField(fieldName);

        if (field is null)
        {
            return fieldName;
        }
        
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        
        return attribute is not null ? attribute.Description : fieldName;
    }

    public static IEnumerable<string> GetDescriptions<TEnum>() where TEnum : Enum =>
        Enum
            .GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => e.GetDescription());
}