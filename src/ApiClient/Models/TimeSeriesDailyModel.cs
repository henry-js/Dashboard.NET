using System.Text.Json.Serialization;
using Skender.Stock.Indicators;

namespace Dashboard.NET.ApiClient.Models;

public partial class TimeSeriesDailyModel
{
    [JsonPropertyName("Meta Data")]
    public MetaData MetaData { get; set; }

    [JsonPropertyName("Time Series (Daily)")]
    public Dictionary<string, Quote> TimeSeriesDaily { get; set; }
}

public partial class MetaData
{
    [JsonPropertyName("1. Information")]
    public string Information { get; set; }

    [JsonPropertyName("2. Symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("3. Last Refreshed")]
    public DateTimeOffset LastRefreshed { get; set; }

    [JsonPropertyName("4. Output Size")]
    public string OutputSize { get; set; }

    [JsonPropertyName("5. Time Zone")]
    public string TimeZone { get; set; }
}

public class Quote
{
    [JsonPropertyName("1. open")]
    public decimal Open { get; set; }

    [JsonPropertyName("2. high")]
    public decimal High { get; set; }

    [JsonPropertyName("3. low")]
    public decimal Low { get; set; }

    [JsonPropertyName("4. close")]
    public decimal Close { get; set; }

    [JsonPropertyName("5. volume")]
    public decimal Volume { get; set; }
}
