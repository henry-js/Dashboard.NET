using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.NET.Cli.Commands;

public class BoardCommand : AsyncCommand<BoardCommandSettings>
{
    private IWeatherService weatherService;
    private ILogger<BoardCommand> logger;
    private IConfiguration config;
    private UserSettings userSettings;

    public BoardCommand(IWeatherService weatherService, ILogger<BoardCommand> logger, IConfiguration config, UserSettings userSettings = null)
    {
        this.weatherService = weatherService;
        this.logger = logger;
        this.config = config;
        this.userSettings = userSettings;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, BoardCommandSettings settings)
    {
        var weather = await weatherService.GetWeatherAsync(userSettings.OpenWeatherSettings.Latitude,
                                                           userSettings.OpenWeatherSettings.Longitude,
                                                           config.GetValue<string>("OPENWEATHER:APPID"),
                                                           new List<string>());
        // TGui.DisplayBoard();
        return 0;
    }
}
