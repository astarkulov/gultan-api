using System.Text.RegularExpressions;

namespace Gultan.Application.Common.Extensions;

public static class StringExtensions
{
    public static string FormatStackTrace(this string stackTrace) => 
        Regex.Replace(stackTrace, @"\r\n|\n|\r", Environment.NewLine);
}