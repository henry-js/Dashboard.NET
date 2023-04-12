using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.Lib.Models;
using Dashboard.NET.Lib.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.NET.Cli.Commands;

public class GetWeatherCommand : AsyncCommand<GetWeatherCommandSettings>
{
    private IWeatherService weatherService;
    private ILogger<GetWeatherCommand> logger;
    private IConfiguration config;
    private UserSettings userSettings;

    public GetWeatherCommand(
        IWeatherService weatherService,
        ILogger<GetWeatherCommand> logger,
        IConfiguration config,
        [NotNull] UserSettings userSettings)
    {
        this.weatherService = weatherService;
        this.logger = logger;
        this.config = config;
        this.userSettings = userSettings;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, GetWeatherCommandSettings settings)
    {
        settings.ApiKey ??= config.GetValue<string>("OPENWEATHER:APPID");
        if (settings.UseSaved)
        {
            UpdateSettingsFromStorage(settings, userSettings.OpenWeatherSettings);
            var forecast = await GetWeather(settings);
            AnsiConsole.WriteLine(JsonSerializer.Serialize(forecast, new JsonSerializerOptions { WriteIndented = true }));
            return 0;
        }

        if (!string.IsNullOrEmpty(settings.City) && !string.IsNullOrEmpty(settings.Country))
        {
            // 2. Use geocoding api to get lat/lon and store in settings obj
            var apiResponse = await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star2)
                .AutoRefresh(false)
                .StartAsync($"Getting coordinates for {settings.City}, {settings.Country}...", async ctx =>
                {
                    ctx.Refresh();
                    var response = weatherService.SetLocation(settings.City, settings.Country, settings.ApiKey);
                    Thread.Sleep(1000);

                    return await response;
                });
            settings.Latitude = apiResponse[0].Lat;
            settings.Longitude = apiResponse[0].Lon;

            AnsiConsole.WriteLine($"Latitude: {settings.Latitude}, Longitude: {settings.Longitude}");
            AnsiConsole.WriteLine("Press any key...");
            Console.ReadKey();
        }

        if ((settings.Latitude is double.NaN) || (settings.Longitude is double.NaN))
        {
            PromptForGeoData();
            return -1;
        }

        // var weatherForecast = await GetWeather(settings);
        return 0;
    }

    private static void PromptForGeoData()
    {
        AnsiConsole.MarkupLine("[red]WARNING[/]");
        AnsiConsole.WriteLine("The api requires latitude & longitude to return an accurate weather forecast.");
        AnsiConsole.WriteLine("Save persistently by setting your city and ISO country code with:");
        AnsiConsole.MarkupLine("    [green]dash set weather country[/] [red]<3-LETTER-ISO>[/]");
        AnsiConsole.MarkupLine("    [green]dash set weather city[/] [red]<city>[/]");
        AnsiConsole.WriteLine("Or run now with:");
        AnsiConsole.MarkupLine("[green]dash weather get --city <city> --country[/] [red]<3-LETTER-ISO>[/]");
    }

    private async Task<WeatherModel> GetWeather(GetWeatherCommandSettings settings)
    {
        return await weatherService.GetWeatherAsync(settings.Latitude, settings.Longitude, settings.ApiKey, settings.Forecast?.ToList() ?? new());
    }

    private void UpdateSettingsFromStorage(GetWeatherCommandSettings settings, OpenWeatherSettings openWeatherSettings)
    {
        settings.ApiKey ??= config.GetValue<string>("OPENWEATHER:APPID");

        if (settings.Latitude is double.NaN)
        {
            settings.Latitude = openWeatherSettings.Latitude;
        }
        if (settings.Longitude is double.NaN)
        {
            settings.Longitude = openWeatherSettings.Longitude;
        }
    }
}
