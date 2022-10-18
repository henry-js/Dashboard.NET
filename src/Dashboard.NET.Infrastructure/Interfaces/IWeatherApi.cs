using Dashboard.NET.Infrastructure.Models;
using Refit;

namespace Dashboard.NET.Infrastructure.Interfaces;

public interface IWeatherApi
{
    [Get(path: "/data/2.5/onecall?lat={lat}&lon={lon}&exclude={exclude}&appid={apiKey}&units={units}")]
    Task<WeatherModel> GetWeather(double lat, double lon, string apiKey, string exclude, string units = "metric");

    [Get(path: "/geo/1.0/direct?q={cityName},{countryCode}&limit=1&appid={apiKey}")]
    Task<List<LocationModel>> GetLocation(string cityName, string countryCode, string apiKey);
}
