using Dashboard.NET.ApiClient.Converters;
using Dashboard.NET.ApiClient.Interfaces;
using Dashboard.NET.ApiClient.Models;

namespace Dashboard.NET.ApiClient.Services;

public interface IStockService
{
    Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey);
    Task<TimeSeriesDailyResult> GetTimeSeriesDailyAsync(string symbol, string apiKey);
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

    public async Task<TimeSeriesDailyResult> GetTimeSeriesDailyAsync(string symbol, string apiKey)
    {
        // TODO - Rework this
        var stringResponse = await _stockApi.GetTimeSeriesDaily("TIME_SERIES_DAILY", symbol, apiKey);
        var alphaVantageResult = new AlphaVantageResult(symbol, stringResponse);
        var converter = new TimeSeriesDailyConverter();
        var converted = converter.Convert(alphaVantageResult);

        return converted;
    }

    private async Task<string> CallApi(string methodName, string symbol, string apiKey)
    {
        return methodName switch
        {
            "GetTimeSeriesDailyAsync" => await _stockApi.GetTimeSeriesDaily("TIME_SERIES_DAILY", symbol, apiKey),
            _ => throw new NotImplementedException()
        };
    }
}

public class AlphaVantageResult
{
    public AlphaVantageResult(string symbol, string? result)
    {
        Succeeded = !string.IsNullOrWhiteSpace(result);
        Symbol = symbol;
        Result = result;
    }

    public bool Succeeded { get; }
    public string Symbol { get; }
    public string Result { get; }
}


public class TimeSeriesDailyResult
{
    public bool Succeeded { get; init; }
    public string Symbol { get; init; }
    public IEnumerable<TimeSeriesDailyModel> Result { get; init; }
}
