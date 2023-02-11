using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Persistence.Context.Interceptors;
using System.Reflection;

namespace People.Infrastructure.Persistence.Context;

public class PeopleContext : DbContext
{
    public const string DEFAULT_SCHEMA = "people";
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public PeopleContext(
		DbContextOptions<PeopleContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
	{
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}
