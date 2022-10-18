namespace Dashboard.NET.Cli.Commands.Settings;
public class GetCommandSettings : CommandSettings
{
    [DefaultValue(true)]
    [CommandOption("--use-saved")]
    public bool UseSaved { get; set; }
}
