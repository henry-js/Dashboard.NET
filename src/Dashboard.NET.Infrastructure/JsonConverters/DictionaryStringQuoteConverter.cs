using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dashboard.NET.Infrastructure.Converters;

public class DictionaryStringQuoteConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert.IsGenericType == false)
        {
            return false;
        }
        if (typeToConvert.GetGenericTypeDefinition() != typeof(Dictionary<,>))
        {
            return false;
        }
        return typeToConvert.GetGenericArguments()[0] == typeof(string);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}