using Dashboard.NET.Infrastructure.Models;
using Dashboard.NET.Infrastructure.Services;
using Refit;

namespace Dashboard.NET.Infrastructure.Interfaces;

public interface IStockApi
{
    [Get("/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}")]
    Task<GlobalQuoteModel> GetGlobalQuote(string symbol, string apiKey);

    [Get("/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={apiKey")]
    Task<TimeSeriesDailyModel> GetTimeSeriesDaily(string symbol, string apiKey);
}
