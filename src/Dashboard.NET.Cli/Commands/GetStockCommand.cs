using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.Infrastructure.Models;
using Dashboard.NET.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.NET.Cli.Commands;

public class GetStockCommand : AsyncCommand<GetStockCommandSettings>
{

    private IStockService stockService;
    private IConfiguration config;
    private ILogger<GetStockCommand> logger;


    public GetStockCommand(IStockService stockService, IConfiguration config, ILogger<GetStockCommand> logger)
    {
        this.stockService = stockService;
        this.config = config;
        this.logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, GetStockCommandSettings settings)
    {
        GlobalQuoteModel quote = await stockService.GetGlobalQuoteAsync("GME", config.GetValue<string>(("ALPHAVANTAGE:APPID")));
        AnsiConsole.WriteLine(JsonSerializer.Serialize(quote, new JsonSerializerOptions { WriteIndented = true}));
        Console.ReadLine();
        return 0;
    }
}
