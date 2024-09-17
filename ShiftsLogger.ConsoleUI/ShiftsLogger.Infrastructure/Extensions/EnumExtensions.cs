﻿using System.ComponentModel;
using System.Reflection;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        if (field is null)
        {
            return value.ToString();
        }
        
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        
        return attribute is not null ? attribute.Description : value.ToString();
    }
}