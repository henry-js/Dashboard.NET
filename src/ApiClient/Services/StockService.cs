using Dashboard.NET.ApiClient.Interfaces;
using Dashboard.NET.ApiClient.Models;

namespace Dashboard.NET.ApiClient.Services;

public interface IStockService
{
    Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey);
    Task<string> GetTimeSeriesDailyAsync(string symbol, string apiKey);
}

public class StockService : IStockService
{
    private readonly IStockApi _stockApi;

    public StockService(IStockApi stockApi)
    {
        _stockApi = stockApi;
    }

    public async Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey)
    {
        var response = await _stockApi.GetGlobalQuote(symbol, apiKey);
        return response;
    }

    public async Task<string> GetTimeSeriesDailyAsync(string symbol, string apiKey)
    {
        var response = await _stockApi.GetTimeSeriesDaily(symbol, apiKey);
        return response;
    }
}
