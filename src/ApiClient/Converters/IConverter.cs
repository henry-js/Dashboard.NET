using Dashboard.NET.ApiClient.Services;

namespace Dashboard.NET.Infrastructure.Converters;

public interface IConverter
{
    TimeSeriesResult Convert(string response);
}
