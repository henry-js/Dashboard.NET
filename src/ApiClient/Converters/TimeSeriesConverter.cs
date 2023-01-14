using System.Globalization;
using CsvHelper;
using Dashboard.NET.ApiClient.Models;
using Dashboard.NET.ApiClient.Services;
using Dashboard.NET.Infrastructure.Converters;

namespace Dashboard.NET.ApiClient.Converters;

public class TimeSeriesConverter : IConverter
{
    public TimeSeriesResult Convert(string response)
    {
        using var reader = new StringReader(response);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<TimeSeriesMap>();

        var timeSeriesDaily = csv.GetRecords<TimeSeries>();

        return new() { Values = timeSeriesDaily.ToList(), Succeeded = true };
    }
}
