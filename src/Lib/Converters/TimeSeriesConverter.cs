using System.Globalization;
using CsvHelper;
using Dashboard.NET.Infrastructure.Converters;
using Dashboard.NET.Lib.Models;
using Dashboard.NET.Lib.Services;

namespace Dashboard.NET.Lib.Converters;

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
