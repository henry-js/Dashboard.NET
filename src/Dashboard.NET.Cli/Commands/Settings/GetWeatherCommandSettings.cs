namespace Dashboard.NET.Cli.Commands.Settings;

public class GetWeatherCommandSettings : GetCommandSettings
{
    [CommandArgument(0, "[Forecast]")]
    public string[] Forecast { get; set; } = new string[5];

    [CommandOption("--latitude", IsHidden = true)]
    public double Latitude { get; set; } = double.NaN;

    [CommandOption("--longitude", IsHidden = true)]
    public double Longitude { get; set; } = double.NaN;

    [CommandOption("-a|--apikey")]
    public string ApiKey { get; set; } = null!;

    [CommandOption("-u|--units")]
    public Units Units { get; set; }

    [CommandOption("--ci|--city")]
    public string City { get; set; } = string.Empty;

    [CommandOption("--co|--country")]
    public string Country { get; set; } = string.Empty;
}

public enum Units
{
    Metric,
    Imperial,
}
