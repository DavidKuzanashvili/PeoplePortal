using System.Globalization;

namespace People.API.Localizer;

public class LocalizerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cultureKey = context.Request.Headers["Accept-Language"];

        if (!string.IsNullOrWhiteSpace(cultureKey))
        {
            if (DoesCultureExist(cultureKey!))
            {
                var culture = new CultureInfo(cultureKey!);

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        await next(context);
    }

    private static bool DoesCultureExist(string cultureName)
    {
        var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
        return CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Any(culture => string.Equals(culture.Name, cultureName,
                                            StringComparison.CurrentCultureIgnoreCase));
    }
}
