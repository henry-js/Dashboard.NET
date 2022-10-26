using Dashboard.NET.ApiClient.Interfaces;
using Dashboard.NET.ApiClient.Services;
using Microsoft.Extensions.Configuration;

namespace Dashboard.NET.Tests.Service.Tests;

public class WeatherServiceTests
{
    private readonly WeatherService _sut;
    private readonly IWeatherApi _weatherApi = Substitute.For<IWeatherApi>();
    private readonly IConfigurationRoot configuration = new ConfigurationBuilder().AddEnvironmentVariables()
                                                                                  .Build();
    private readonly double Lat = 52.3881;
    private readonly double Lon = -2.2479;

    public WeatherServiceTests(WeatherService sut)
    {
        _sut = new WeatherService(_weatherApi);
    }

    // [Fact]
    // public async Task GetWeatherAsync_ShouldReturnAll_WhenNoForecastIsExcluded()
    // {
    //     // Arrange
    //     var apiKey = configuration.GetValue<string>("OPENWEATHER:APPID");
    //     // Act
    //     _sut.GetWeatherAsync(Lat, Lon, apiKey, new List<string>());
    //
    //     // Assert
    // }
}