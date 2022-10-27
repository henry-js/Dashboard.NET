﻿using Dashboard.NET.ApiClient.Models;
using Dashboard.NET.ApiClient.Services;
using Refit;

namespace Dashboard.NET.ApiClient.Interfaces;

public interface IAlphaVantageApi
{
    [Get("/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}")]
    Task<GlobalQuoteModel> GetGlobalQuote(string symbol, string apiKey);

    [Get("/query?function={function}&symbol={symbol}&apikey={apiKey}&datatype=csv")]
    Task<string> GetTimeSeriesDaily(string function, string symbol, string apiKey);
}
