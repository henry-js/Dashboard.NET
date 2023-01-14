using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using Skender.Stock.Indicators;

namespace Dashboard.NET.ApiClient.Models;

#nullable disable
public partial class TimeSeries
{
    public DateOnly TimeStamp { get; set; }
    [JsonPropertyName("1. open")]
    public decimal Open { get; set; }

    [JsonPropertyName("2. high")]
    public decimal High { get; set; }

    [JsonPropertyName("3. low")]
    public decimal Low { get; set; }

    [JsonPropertyName("4. close")]
    public decimal Close { get; set; }
    public decimal AdjustedClose { get; set; }

    [JsonPropertyName("5. volume")]
    public int Volume { get; set; }
    public decimal DividendAmount { get; set; }
    public double SplitCoefficient { get; set; }
}

#nullable enable
