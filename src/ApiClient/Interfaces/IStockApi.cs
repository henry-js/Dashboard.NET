using Dashboard.NET.ApiClient.Models;
using Dashboard.NET.ApiClient.Services;
using Refit;

namespace Dashboard.NET.ApiClient.Interfaces;

public interface IStockApi
{
    [Get("/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}")]
    Task<GlobalQuoteModel> GetGlobalQuote(string symbol, string apiKey);

    [Get("/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={apiKey}&datatype=csv")]
    Task<string> GetTimeSeriesDaily(string symbol, string apiKey);
}
