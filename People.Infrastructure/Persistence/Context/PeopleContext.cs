using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace People.Infrastructure.Persistence.Context;

public class PeopleContext : DbContext
{
    public const string DEFAULT_SCHEMA = "people";

	public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
