using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dashboard.NET.Infrastructure.JsonConverters;

public class QuoteConverter : JsonConverter<object>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.Number => reader.GetDouble(),
            JsonTokenType.String when reader.TryGetDateTime(out DateTime datetime) => datetime,
            JsonTokenType.String => reader.GetString()!,
            _ => JsonDocument.ParseValue(ref reader).RootElement.Clone()
        };

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)=>
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
}