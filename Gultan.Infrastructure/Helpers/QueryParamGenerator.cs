using System.Reflection;
using Newtonsoft.Json;

namespace Gultan.Infrastructure.Helpers;

public static class QueryParamGenerator
{
    public static string Generate<T>(T parameters)
    {
        var properties = typeof(T).GetProperties();
        var result = string.Empty;
        
        foreach (var property in properties)
        {
            var jsonPropertyAttribute = property.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonPropertyAttribute != null)
            {
                var propertyName = jsonPropertyAttribute.PropertyName;
                var propertyValue = property.GetValue(parameters)?.ToString();
                result += $"{propertyName}={propertyValue}&";
            }
        }

        return result.Substring(0, result.Length-1);
    }
}