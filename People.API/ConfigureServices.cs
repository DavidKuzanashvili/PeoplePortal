using FluentValidation;
using Microsoft.Extensions.Localization;
using People.API.Localizer;

namespace People.API;

internal static class ConfigureServices
{
    public static IServiceCollection AddPeopleAPIServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);

        services.AddLocalization();
        services.AddSingleton<LocalizerMiddleware>();
        services.AddDistributedMemoryCache();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        return services;
    }
}
