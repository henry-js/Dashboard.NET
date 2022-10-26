using D20Tek.Spectre.Console.Extensions.Injection;
using Dashboard.NET.Cli.Commands;
using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.ApiClient.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;

IConfiguration config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();

services.AddScoped<IConfiguration>(_ => new ConfigurationBuilder().AddEnvironmentVariables().Build())
        .AddUserSettings()
        .AddWeatherService()
        .AddLogging(configure =>
        configure.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            // .WriteTo.Console()
            // .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger()
        ));

var registrar = new DependencyInjectionTypeRegistrar(services);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.PropagateExceptions();
    config.CaseSensitivity(CaseSensitivity.None)
          .SetApplicationName("Dashboard.NET")
          .ValidateExamples();
    config.AddBranch<GetCommandSettings>("get", get =>
    {
        get.AddCommand<GetWeatherCommand>("weather");
        get.AddCommand<GetStockCommand>("stock");
    });
    config.AddCommand<BoardCommand>("board");
});

try
{
    return await app.RunAsync(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return 1;
}
