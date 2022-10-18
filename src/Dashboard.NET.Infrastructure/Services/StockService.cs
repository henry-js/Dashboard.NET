using Dashboard.NET.Infrastructure.Interfaces;
using Dashboard.NET.Infrastructure.Models;

namespace Dashboard.NET.Infrastructure.Services;

public interface IStockService
{
    Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey);

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
}
