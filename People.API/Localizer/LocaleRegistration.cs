using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace People.API.Localizer;

internal static class RegisterLocalizer
{
    public static WebApplication RegisterLocale(this WebApplication app)
    {
        var options = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"))
        };

        app.UseRequestLocalization(options);
        app.UseStaticFiles();
        app.UseMiddleware<LocalizerMiddleware>();

        return app;
    }
}
