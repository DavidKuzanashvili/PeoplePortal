namespace People.Infrastructure.Extensions;

public static  class StringExtensions
{
    public static bool ContinsIgnoreCase(this string name, string val)
    {
        return name.Contains(val, StringComparison.InvariantCultureIgnoreCase);
    }
}
