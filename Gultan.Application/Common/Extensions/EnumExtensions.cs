using System.Text.RegularExpressions;

namespace Gultan.Application.Common.Extensions;

public static class EnumExtensions
{
    public static string ToStringSpaceCamelCase(this Enum input)
    {
        return Regex.Replace(input.ToString(), "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
    }
}