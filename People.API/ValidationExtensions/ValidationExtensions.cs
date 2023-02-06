using FluentValidation;
using System.Globalization;

namespace People.API.ValidationExtensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> MatchesValidAlphabet<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;

        if (culture.Name == "en-US")
        {
            return ruleBuilder
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("invalid.alphabet.onlylatin");
        }

        return ruleBuilder
            .Matches(@"[\u10A0-\u10FF]")
            .WithMessage("invalid.alphabet.onlygeo");
    }
}
