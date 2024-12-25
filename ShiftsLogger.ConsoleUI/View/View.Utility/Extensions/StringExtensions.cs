using System.Text;
using Spectre.Console;
using View.Entity;

namespace View.Utility.Extensions;

public static class StringExtensions
{
    public static string[] SplitCamelCase(this string[] cameCaseStrings) =>
        cameCaseStrings.Select(str => str.SplitCamelCase()).ToArray();
    
    public static string SplitCamelCase(this string camelCaseString)
    {
        var builder = new StringBuilder();

        foreach (char character in camelCaseString)
        {
            if (char.IsUpper(character))
            {
                builder.Append(' ');
            }

            builder.Append(character);
        }

        return builder.ToString().Trim();
    }
    
    public static StyledString[] ApplyStyle(this string[] strings, Style? textStyle, HorizontalAlignment alignment = HorizontalAlignment.Left) =>
        strings.Select(str => str.ApplyStyle(textStyle, alignment)).ToArray();

    public static StyledString ApplyStyle(this string str, Style? textStyle, HorizontalAlignment alignment = HorizontalAlignment.Left) =>
        new (str, textStyle, alignment);
}