using System.Reflection;
using Dashboard.NET.ApiClient.Models;
using Dashboard.NET.ApiClient.Services;
using Dashboard.NET.Cli.Commands.Settings;
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
        _stockService = stockService;
        _config = config;
        _logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, GetStockCommandSettings settings)
    {
        var apiKey = _config.GetValue<string>("ALPHAVANTAGE:APPID");
        var result = await _stockService.GetTimeSeriesAsync("GME", apiKey);
        var table = new Table();
        if (!result.Succeeded)
        {
            AnsiConsole.WriteLine("Attempt to query was unsuccessful");
        }
        else
        {
            var headers = result.Values.First().GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToArray();
            foreach (var header in headers)
            {
                table.AddColumn(header);
            }
            foreach (TimeSeries row in result.Values)
            {
                var rowValues = headers.Select(col => row.GetType().GetProperty(col)?.GetValue(row)?.ToString() ?? "");
                table.AddRow(rowValues.ToArray());
            }
        }
        AnsiConsole.Write(table);
        Console.ReadLine();
        return 0;
    }
}
