using People.Domain.Exceptions;

namespace People.Domain.Helpers;

internal static class Guards
{
    internal static void NotNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value)) 
        {
            throw new DomainException($"'{paramName}' cannot be null or whitespace.", paramName);
        }
    }

    internal static TEnum TryParse<TEnum>(string? value, string paramName) where TEnum : struct
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"'{paramName}' cannot be null or whitespace.", paramName);
        }

        var isValid = Enum.TryParse(value, true, out TEnum result);

        return !isValid 
            ? throw new DomainException($"'{paramName}' deosn't exists.", paramName)
            : result;
    }
}
