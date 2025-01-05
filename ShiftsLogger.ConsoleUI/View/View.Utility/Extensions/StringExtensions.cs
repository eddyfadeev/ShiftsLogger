using System.Text;

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
}