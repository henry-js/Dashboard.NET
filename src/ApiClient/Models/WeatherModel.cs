using System.Text.Json.Serialization;

namespace Dashboard.NET.ApiClient.Models;
#pragma warning disable CS8618
public partial class WeatherModel
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    public long TimezoneOffset { get; set; }
    public Current Current { get; set; }
    public List<Minutely> Minutely { get; set; }
    public List<Current> Hourly { get; set; }
    public List<Daily> Daily { get; set; }
    public List<Alert> Alerts { get; set; }
}

public partial class Alert
{
    public string SenderName { get; set; }
    public string Event { get; set; }
    public long Start { get; set; }
    public long End { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
}

public partial class Current
{
    public long Dt { get; set; }
    public long? Sunrise { get; set; }
    public long? Sunset { get; set; }
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public long Pressure { get; set; }
    public long Humidity { get; set; }
    public double DewPoint { get; set; }
    public double Uvi { get; set; }
    public long Clouds { get; set; }
    public long Visibility { get; set; }
    public double WindSpeed { get; set; }
    public long WindDeg { get; set; }
    public double WindGust { get; set; }
    public List<Weather> Weather { get; set; }
    public double? Pop { get; set; }
    public Rain Rain { get; set; }
}

public partial class Rain
{
    public double The1H { get; set; }
}

public partial class Weather
{
    public long Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public partial class Daily
{
    public long Dt { get; set; }
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
    public long Moonrise { get; set; }
    public long Moonset { get; set; }
    public double MoonPhase { get; set; }
    public Temp Temp { get; set; }
    public FeelsLike FeelsLike { get; set; }
    public long Pressure { get; set; }
    public long Humidity { get; set; }
    public double DewPoint { get; set; }
    public double WindSpeed { get; set; }
    public long WindDeg { get; set; }
    public double WindGust { get; set; }
    public List<Weather> Weather { get; set; }
    public long Clouds { get; set; }
    public double Pop { get; set; }
    public double Uvi { get; set; }
    public double? Rain { get; set; }
}

public partial class FeelsLike
{
    public double Day { get; set; }
    public double Night { get; set; }
    public double Eve { get; set; }
    public double Morn { get; set; }
}

public partial class Temp
{
    public double Day { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double Night { get; set; }
    public double Eve { get; set; }
    public double Morn { get; set; }
}

public partial class Minutely
{
    public long Dt { get; set; }
    public long Precipitation { get; set; }
}
#pragma warning restore CS8618

public enum Description
{
    BrokenClouds,
    ClearSky,
    FewClouds,
    LightRain,
    ModerateRain,
    OvercastClouds,
    ScatteredClouds
};

public enum Main
{
    Clear,
    Clouds,
    Rain
};
