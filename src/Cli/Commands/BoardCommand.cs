using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.NET.Cli.Commands;

public class BoardCommand : AsyncCommand<BoardCommandSettings>
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<BoardCommand> _logger;
    private readonly IConfiguration _config;
    private readonly UserSettings _userSettings;

    public BoardCommand(IWeatherService weatherService, ILogger<BoardCommand> logger, IConfiguration config, UserSettings userSettings)
    {
        this._weatherService = weatherService;
        this._logger = logger;
        this._config = config;
        this._userSettings = userSettings;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, BoardCommandSettings settings)
    {
        var weather = await _weatherService.GetWeatherAsync(_userSettings.OpenWeatherSettings.Latitude,
                                                           _userSettings.OpenWeatherSettings.Longitude,
                                                           _config.GetValue<string>("OPENWEATHER:APPID"),
                                                           new List<string>());
        // TGui.DisplayBoard();
        return 0;
    }
}
