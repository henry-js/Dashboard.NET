using Dashboard.NET.Lib.Services;
using Dashboard.NET.Lib.Models;
using Refit;

namespace Dashboard.NET.Lib.Interfaces;

public interface IAlphaVantageApi
{
    [Get("/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}")]
    Task<GlobalQuoteModel> GetGlobalQuote(string symbol, string apiKey);

    [Get("/query?function={function}&symbol={symbol}&apikey={apiKey}&outputsize=full&datatype=csv")]
    Task<string> GetTimeSeries(string function, string symbol, string apiKey);
}

// TODO : Move to vertical slice Stock folder
