using System.Globalization;
using CsvHelper;
using Dashboard.NET.ApiClient.Models;
using Dashboard.NET.ApiClient.Services;
using Dashboard.NET.ApiClient.Interfaces;

namespace Dashboard.NET.ApiClient.Converters;

public class TimeSeriesDailyConverter : IConverter
{
    public TimeSeriesDailyResult Convert(AlphaVantageResult input)
    {
        using var reader = new StringReader(input.Result);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var timeSeriesDaily = csv.GetRecords<TimeSeriesDailyModel>();

        return new TimeSeriesDailyResult() { Result = timeSeriesDaily.ToList(), Succeeded = true };
    }
}
