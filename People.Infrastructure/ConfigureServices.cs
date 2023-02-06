using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.Application.Cities.Repositories;
using People.Application.Common;
using People.Application.Files;
using People.Application.People.Repositories;
using People.Infrastructure.Files;
using People.Infrastructure.Persistence.Context;
using People.Infrastructure.Persistence.Repositories;

namespace People.Infrastructure;

public static class ConfigureServices
{
    public const string DbConnectionStringName = "PeopleDb";

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbConnectionStr = configuration.GetConnectionString(DbConnectionStringName); 
        services.AddDbContext<PeopleContext>(builder =>
        {
            builder.UseSqlServer(dbConnectionStr, options =>
            {
                options.MigrationsAssembly("People.Infrastructure")
                    .MigrationsHistoryTable(
                        "__EFMigrationHistory",
                        PeopleContext.DEFAULT_SCHEMA)
                    .EnableRetryOnFailure();
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPeopleRepository, PeopleRepository>();
        services.AddScoped<ICitiesRepository, CitiesRepository>();

        services.AddScoped<IFileStorage, FileStorage>();
        services.AddScoped<ICsvFileBuilder, CsvFileBuilder>();

        return services;
    }
}