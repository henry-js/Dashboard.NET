using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using Skender.Stock.Indicators;

namespace Dashboard.NET.ApiClient.Models;

#nullable disable
public partial class TimeSeriesDailyModel
{
    [Name("timestamp")] public DateTime TimeStamp { get; set; }
    [JsonPropertyName("1. open")]
    [Name("open")]
    public decimal Open { get; set; }

    [JsonPropertyName("2. high")]
    [Name("high")]
    public decimal High { get; set; }

    [JsonPropertyName("3. low")]
    [Name("low")]
    public decimal Low { get; set; }

    [JsonPropertyName("4. close")]
    [Name("close")]
    public decimal Close { get; set; }

    [JsonPropertyName("5. volume")]
    [Name("volume")]
    public decimal Volume { get; set; }
}

#nullable enable