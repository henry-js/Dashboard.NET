using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.ApiClient.Models;
using Dashboard.NET.ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.NET.Cli.Commands;

public class GetStockCommand : AsyncCommand<GetStockCommandSettings>
{

    private readonly IStockService _stockService;
    private readonly IConfiguration _config;
    private ILogger<GetStockCommand> _logger;


    public GetStockCommand(IStockService stockService, IConfiguration config, ILogger<GetStockCommand> logger)
    {
        this._stockService = stockService;
        this._config = config;
        this._logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, GetStockCommandSettings settings)
    {
        var apiKey = _config.GetValue<string>("ALPHAVANTAGE:APPID");
        var quote = await _stockService.GetTimeSeriesDailyAsync("GME", apiKey);
        AnsiConsole.WriteLine(JsonSerializer.Serialize(quote, new JsonSerializerOptions { WriteIndented = true}));
        Console.ReadLine();
        return 0;
    }
}
