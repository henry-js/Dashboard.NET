using Dashboard.NET.ApiClient.Services;

namespace Dashboard.NET.ApiClient.Interfaces;

public interface IConverter
{
    TimeSeriesDailyResult Convert(AlphaVantageResult csvInput);
}
