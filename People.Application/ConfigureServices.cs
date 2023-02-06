using Microsoft.Extensions.DependencyInjection;
using People.Application.People;

namespace People.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPeopleService, PeopleService>();

        return services;
    }
}
