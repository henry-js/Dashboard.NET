using Dashboard.NET.ApiClient.Interfaces;
using Dashboard.NET.ApiClient.Models;

namespace Dashboard.NET.ApiClient.Services;

public interface IWeatherService
{
    Task<WeatherModel> GetWeatherAsync(double lat, double lon, string apiKey, List<string> foreCast);
    Task<List<LocationModel>> SetLocation(string cityName, string countryCode, string apiKey);
}

public class WeatherService : IWeatherService
{
    private readonly IWeatherApi _weatherApi;

    private static List<string> ExcludeCategories => new() { "daily", "minutely", "hourly" /*, "current", "alerts" */};

    public WeatherService(IWeatherApi weatherApi)
    {
        _weatherApi = weatherApi;
    }
    public async Task<WeatherModel> GetWeatherAsync(double lat, double lon, string apiKey, List<string> forecast)
    {
        // forecast.ForEach(val => excludeCategories.Remove(val));
        string exclude = string.Join(",", ExcludeCategories);
        return await _weatherApi.GetWeather(lat, lon, apiKey, exclude);
    }

    // public async Task<WeatherModel> GetWeather(OpenWeatherSettings openWeatherSettings)
    // {
    //     var excludeCategories = new List<string>(ExcludeCategories);
    //     // forecast.ForEach(val => excludeCategories.Remove(val));

    //     string exclude = string.Join(",", excludeCategories);
    //     var response = await WeatherApi.GetWeather(openWeatherSettings.Latitude, openWeatherSettings.Longitude, openWeatherSettings.apiKey);
    //     return response;
    // }

    public async Task<List<LocationModel>> SetLocation(string cityName, string countryCode, string apiKey)
    {
        var response = await _weatherApi.GetLocation(cityName, countryCode, apiKey);
        // TODO: Change to return success/failure operation result
        return response;
    }
}
