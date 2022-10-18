using Dashboard.NET.Infrastructure.Models;
using Dashboard.NET.Infrastructure.Services;
using Refit;

namespace Dashboard.NET.Infrastructure.Interfaces;

public interface IStockApi
{
    [Get("/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}")]
    Task<GlobalQuoteModel> GetGlobalQuote(string symbol, string apiKey);
}

class StockApi
{
    public StockApi()
    {
        var stockApi = RestService.For<IStockApi>("https://www.alphavantage.co");
        
    }
}