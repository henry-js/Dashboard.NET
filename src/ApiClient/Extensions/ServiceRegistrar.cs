using System.Text.Json;
using System.Text.Json.Serialization;
using Dashboard.NET.ApiClient.Interfaces;
using Dashboard.NET.ApiClient.Services;
using Microsoft.Extensions.DependencyInjection;
using nucs.JsonSettings;
using Refit;

namespace Dashboard.NET.ApiClient.Extensions;

public static class ServiceRegistrar
{
    public static IServiceCollection AddUserSettings(this IServiceCollection services)
    {
        string directory = Directory.GetCurrentDirectory();
        if (File.Exists($"{directory}\\userSettings.json"))
        {
            services.AddTransient(_ =>
            {
                var settings = JsonSettings.Load<UserSettings>($"{directory}\\userSettings.json");
                settings.OpenWeatherSettings.City = "London";
                settings.Save();
                return settings;
            });
        }
        else
        {
            services.AddTransient(_ =>
            {
                var settings = JsonSettings.Construct<UserSettings>();
                settings.OpenWeatherSettings.City = "Kidderminster";
                settings.Save();
                return settings;
            });
        }

        return services;
    }

    public static IServiceCollection AddWeatherService(this IServiceCollection services)
    {
        return services.AddSingleton<IWeatherService, WeatherService>()
            .AddRefitClient<IWeatherApi>(new RefitSettings())
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.openweathermap.org"))
            .Services
                
            .AddSingleton<IStockService, StockService>()
            .AddRefitClient<IStockApi>((_) =>
            {
                var options = new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };
                return new RefitSettings
                {
                    ContentSerializer = new SystemTextJsonContentSerializer(options)
                    
                };
            })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.alphavantage.co"))
            .Services;
    }
}
