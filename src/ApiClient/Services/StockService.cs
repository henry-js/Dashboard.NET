using System.Globalization;
using System.Reflection;
using System.Text;
using CsvHelper;
using Dashboard.NET.ApiClient.Constants;
using Dashboard.NET.ApiClient.Interfaces;
using Dashboard.NET.ApiClient.Models;
using Refit;

namespace Dashboard.NET.ApiClient.Services;

public interface IStockService
{
    Task<GlobalQuoteModel> GetGlobalQuoteAsync(string symbol, string apiKey);
    Task<string> GetTimeSeriesDailyAsync(string symbol, string apiKey);
}

public class StockService : IStockService
{
    private readonly IAlphaVantageApi _stockApi;

    public StockService(IAlphaVantageApi stockApi)
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
        var response = await _stockApi.GetTimeSeriesDaily(nameof(GetTimeSeriesDailyAsync), symbol, apiKey);

        var success = !string.IsNullOrWhiteSpace(response);
        // Save csv
        var timeSeriesCsvPath = Path.Combine(Directory.GetCurrentDirectory(), StockConstants.TimeSeriesDailyCsvPath);
        if (!File.Exists(StockConstants.TimeSeriesDailyCsvPath))
        {
            Directory.CreateDirectory(timeSeriesCsvPath);
        }
        File.WriteAllText(StockConstants.TimeSeriesDailyCsvPath + symbol + ".csv", response);


        using (var reader = new StreamReader(response))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //    using (var csv= new CsvReader())

            return string.Empty;
    }

    // private async Task<string> CallApi(string methodName, string symbol, string apiKey)
    // {
    //     return methodName switch
    //     {
    //         "GetTimeSeriesDailyAsync" => await _stockApi.GetTimeSeriesDaily("TIME_SERIES_DAILY", symbol, apiKey),
    //         _ => throw new NotImplementedException()
    //     };
    // }
}

// public class StockApi
// {
//     private readonly IAlphaVantageApi _iAlphaVantageApi;

//     public StockApi(IAlphaVantageApi iAlphaVantageApi)
//     {
//         _iAlphaVantageApi = iAlphaVantageApi;
//     }

//     private async Task<StockResponse> CallApi(string methodName, string symbol, string apiKey)
//     {
//         var response = methodName switch
//         {
//             "GetTimeSeriesDailyAsync" => await _iAlphaVantageApi.GetTimeSeriesDaily("TIME_SERIES_DAILY", symbol, apiKey),
//             _ => throw new NotImplementedException()
//         };
//         var success = !string.IsNullOrWhiteSpace(response);

//         if (success)
//         {
//             return new StockResponse(success, symbol, response);
//         }
//     }
// }

public class StockResponse
{
    public StockResponse(bool succeeded, string symbol, string response)
    {
        Succeeded = succeeded;
        Symbol = symbol;
        Response = response;
    }

    public bool Succeeded { get; }
    public string Symbol { get; }
    public string Response { get; }
}
