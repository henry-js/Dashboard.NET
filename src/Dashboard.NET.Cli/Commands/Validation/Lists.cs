using ISO3166;

namespace Dashboard.NET.Cli.Commands.Validation;

public static class Lists
{
    public static Country[] Countries => Country.List;
}