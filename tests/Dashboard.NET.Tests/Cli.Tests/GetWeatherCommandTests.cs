using Dashboard.NET.Cli.Commands;
using Dashboard.NET.Cli.Commands.Settings;
using Dashboard.NET.ApiClient;
using Dashboard.NET.ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using nucs.JsonSettings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Dashboard.NET.Tests.Cli.Tests;

public class GetWeatherCommandTests
{
    private readonly IRemainingArguments _remainingArgs = new Mock<IRemainingArguments>().Object;
    private readonly UserSettings _userSettings;

    public GetWeatherCommandTests()
    {
        _userSettings = JsonSettings.Construct<UserSettings>();
    }

    [Fact]
    public async Task Execute_ShouldWarnWhenNoArgumentsProvided()
    {
        // arrange
        var service = new Mock<IWeatherService>().Object;
        var myConfig = new Dictionary<string, string>
        {
            { "OPENWEATHER:APPID", "An APIKEY"}
        };
        var configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(myConfig)
                                .Build();
        var logger = new NullLogger<GetWeatherCommand>();
        var command = new GetWeatherCommand(service, logger, configuration, _userSettings);
        var settings = new GetWeatherCommandSettings();
        var context = new CommandContext(_remainingArgs, "get", null);
        AnsiConsole.Record();

        // act
        var result = await command.ExecuteAsync(context, settings);
        var text = AnsiConsole.ExportText();

        // assert
        result.Should().Be(-1, because: "Unsuccessful commands return -1");
        text.Should().Contain("WARNING", because: "If coordinates are not set, warn user");
    }
}
