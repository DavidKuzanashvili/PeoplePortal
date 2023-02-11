using Microsoft.EntityFrameworkCore;
using People.API.Seeding.Dtos;
using People.Domain.Entities;
using People.Infrastructure.Persistence.Context;
using System.Text.Json;

namespace People.API.Seeding;

public class SeedingHostedService : BackgroundService
{
    private const string TRUNCATE = "TRUNCATE";
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SeedingHostedService> _logger;

    public SeedingHostedService(
        IServiceProvider serviceProvider,
        ILogger<SeedingHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting seeding...");

        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<PeopleContext>();

            await dbContext.Database.MigrateAsync(cancellationToken);

            var dbCleanupOption = Environment.GetEnvironmentVariable("DB_CLEANUP_OPTION");

            if (!string.IsNullOrWhiteSpace(dbCleanupOption) && dbCleanupOption == TRUNCATE)
            {
                await TruncateAsync(dbContext, cancellationToken);
            }

            var citiesCount = await dbContext.Set<City>().CountAsync();
            if (citiesCount == 0)
            {
                await AddEntitiesAsync<CitySeedDto, City>(
                    "Seeding/Data/cities.json",
                    dbContext,
                    TransformCity.Transform,
                    cancellationToken);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Seeding failed!");
            throw;
        }
    }

    public async Task AddEntitiesAsync<TIn, TOut>(
        string filePath,
        PeopleContext dbContext,
        Func<TIn, TOut> transform,
        CancellationToken cancellationToken) where TOut : class
    {
        var dbSet = dbContext.Set<TOut>();
        var dtos = await ReadJsonAsync<TIn>(filePath, cancellationToken);
        dbSet.AddRange(dtos
            .Select(d => transform(d))
            .ToArray());
    }

    private async Task<T[]> ReadJsonAsync<T>(string path, CancellationToken cancellationToken)
    {
        var jsonString = await File.ReadAllTextAsync(path, cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T[]>(jsonString, options)?? Array.Empty<T>();
    }

    private async Task TruncateAsync(
        PeopleContext dbContext,
        CancellationToken cancellationToken)
    {
        var dropForeignKyesQuery = @"
                ALTER TABLE [people].[TB_People] 
                DROP CONSTRAINT [FK_TB_People_TB_Cities_City_id]
                ALTER TABLE [people].[TB_PhoneNumbers] 
                DROP CONSTRAINT [FK_TB_PhoneNumbers_TB_People_Person_id]
                ALTER TABLE [people].[TB_RelatedPeople] 
                DROP CONSTRAINT [FK_TB_RelatedPeople_TB_People_Person_id]";

        var addForeignKeysQuery = @"
                ALTER TABLE [people].[TB_People]
                WITH CHECK ADD CONSTRAINT [FK_TB_People_TB_Cities_City_id] 
                FOREIGN KEY([City_id])
                REFERENCES [people].[TB_Cities] ([_id])
                ALTER TABLE [people].[TB_People] 
                CHECK CONSTRAINT [FK_TB_People_TB_Cities_City_id]
                ALTER TABLE [people].[TB_PhoneNumbers] 
                WITH CHECK ADD CONSTRAINT [FK_TB_PhoneNumbers_TB_People_Person_id] 
                FOREIGN KEY([Person_id])
                REFERENCES [people].[TB_People] ([_id])
                ON DELETE CASCADE
                ALTER TABLE [people].[TB_PhoneNumbers] 
                CHECK CONSTRAINT [FK_TB_PhoneNumbers_TB_People_Person_id]
                ALTER TABLE [people].[TB_RelatedPeople] 
                WITH CHECK ADD  CONSTRAINT [FK_TB_RelatedPeople_TB_People_Person_id] 
                FOREIGN KEY([Person_id])
                REFERENCES [people].[TB_People] ([_id])
                ALTER TABLE [people].[TB_RelatedPeople] 
                CHECK CONSTRAINT [FK_TB_RelatedPeople_TB_People_Person_id]";

        await dbContext.Database.ExecuteSqlRawAsync(
            @$"
                {dropForeignKyesQuery}
                TRUNCATE TABLE [people].[TB_People]                    
                TRUNCATE TABLE [people].[TB_RelatedPeople]
                TRUNCATE TABLE [people].[TB_PhoneNumbers]
                TRUNCATE TABLE [people].[TB_Cities]
                {addForeignKeysQuery}",
            cancellationToken);
    }
}
