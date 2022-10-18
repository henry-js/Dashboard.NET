using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using nucs.JsonSettings;

namespace Dashboard.NET.Infrastructure;

public class UserSettings : JsonSettings
{
    public override string FileName { get; set; } = "userSettings.json";
    [NotNull]
    public OpenWeatherSettings OpenWeatherSettings { get; set; } = new()
    {
    };

    public UserSettings()
    {
    }
    public UserSettings(string fileName) : base(fileName)
    {
    }
}

public record class OpenWeatherSettings : IOpenWeatherSettings
{
    public double Latitude { get; set; } = double.NaN;
    public double Longitude { get; set; } = double.NaN;

    public string City { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;

    // public ExclusionCategory? ExclusionCategory { get; set; }

    // public Units Units { get; set; }
    public Dictionary<string, string> ToDict()
    {
        return GetType()
        .GetProperties()
        .ToDictionary(keySelector: prop => prop.Name, elementSelector: el => el.GetValue(this)?.ToString() ?? "null");
    }
}
public interface IOpenWeatherSettings
{
    public Dictionary<string, string> ToDict();
}

public record ServiceSettingsBase
{
}
