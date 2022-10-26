using ISO3166;

namespace Dashboard.NET.Cli.Commands.Validation;

public class ValidateCountryCodeAttribute : ParameterValidationAttribute
{
    private static List<Country> Countries => Country.List.ToList();
#nullable disable
    public ValidateCountryCodeAttribute() : base(errorMessage: null)
    {

    }
#nullable enable
    public override ValidationResult Validate(CommandParameterContext context)
    {
        var values = context.Value is string countryCode ? (IsString: true, IsCountryCode: false) : (IsString: false, IsCountryCode: false);

        values.IsCountryCode = Countries.Exists(country => country.ThreeLetterCode == (context.Value as string)?.ToUpper() || country.TwoLetterCode == (context.Value as string)?.ToUpper());

        return values switch
        {
            { IsString: true, IsCountryCode: true } => ValidationResult.Success(),
            { IsString: true, IsCountryCode: false } => ValidationResult.Error(
                $"{context.Parameter.PropertyName} ({context.Value}) is not a valid country code"
            ),
            _ => ValidationResult.Error(
                $"Invalid {context.Parameter.PropertyName} ({context.Value ?? "<null>"}) specified."
                ),
        };
    }
}
