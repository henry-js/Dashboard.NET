using System.Text.Json.Serialization;
using Skender.Stock.Indicators;

namespace Dashboard.NET.ApiClient.Models;

#nullable disable
public partial class TimeSeriesDailyModel
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

#nullable enable