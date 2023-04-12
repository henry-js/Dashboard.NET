using Dashboard.NET.Infrastructure.Converters;
using Dashboard.NET.Lib.Converters;
using Dashboard.NET.Lib.Interfaces;
using Dashboard.NET.Lib.Models;

namespace Dashboard.NET.Lib.Services;

public interface IStockService
{
    Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey);
    Task<TimeSeriesResult> GetTimeSeriesAsync(string symbol, string apiKey);
}

public class StockService : IStockService
{
    private readonly IAlphaVantageApi _stockApi;
    private readonly IConverter _converter;

    public StockService(IAlphaVantageApi stockApi)
    {
        _stockApi = stockApi;
    }

    public async Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey)
    {
        var response = await _stockApi.GetGlobalQuote(symbol, apiKey);
        return response;
    }

    public async Task<TimeSeriesResult> GetTimeSeriesAsync(string symbol, string apiKey)
    {
        // TODO - Rework this
        var stringResponse = await _stockApi.GetTimeSeries("TIME_SERIES_DAILY_ADJUSTED", symbol, apiKey);
        var converter = new TimeSeriesConverter();
        var converted = converter.Convert(stringResponse);

        return converted;
    }

}


public class TimeSeriesResult
{
    public bool Succeeded { get; init; }
    public string Symbol { get; init; }
    public IEnumerable<TimeSeries> Values { get; init; }
    public string FailureMessage { get; set; }
}
