using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Dashboard.NET.Lib.Models;

namespace Dashboard.NET.Lib.Converters;

public class TimeSeriesMap : ClassMap<TimeSeries>
{
    public TimeSeriesMap()
    {
        Map(m => m.TimeStamp).Name("timestamp").TypeConverter<DateOnlyConverter>();
        Map(m => m.Open).Name("open");
        Map(m => m.High).Name("high");
        Map(m => m.Low).Name("low");
        Map(m => m.Close).Name("close");
        Map(m => m.Volume).Name("volume");
        Map(m => m.AdjustedClose).Name("adjusted_close");
        Map(m => m.DividendAmount).Name("dividend_amount");
        Map(m => m.SplitCoefficient).Name("split_coefficient");
    }
}
