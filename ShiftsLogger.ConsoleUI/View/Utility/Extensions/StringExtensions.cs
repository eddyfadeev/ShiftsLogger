using System.Text;

namespace Utility.Extensions;

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
    
    public static string[] ColorizeSelectedOption(this string[] strings) =>
        strings.Select(str => str.ColorizeSelectedOption()).ToArray();

    public static string ColorizeSelectedOption(this string str) =>
        $"[gold3_1]{str}[/]";
    
    public static string[] DecolorizeSelectedOption(this string[] strings) =>
        strings.Select(str => str.DecolorizeSelectedOption()).ToArray();
    
    public static string DecolorizeSelectedOption(this string str) =>
        str.Replace("[gold3_1]", "").Replace("[/]", "");
}