using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using People.Infrastructure.Persistence.Context;
using System.Reflection;

namespace People.Infrastructure.Persistence;

internal class PeopleContextFactory : IDesignTimeDbContextFactory<PeopleContext>
{
    public PeopleContext CreateDbContext(string[] args)
    {
        var connStr = "Server=.;Initial Catalog=Db_People;Integrated Security=true;";
        if (args is not null && args.Length > 0 && args[0].StartsWith("conn="))
        {
            connStr = args[0][5..];
        }

        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        var optionsBuilder = new DbContextOptionsBuilder<PeopleContext>()
            .UseSqlServer(connStr, opts =>
            {
                opts
                    .MigrationsAssembly(assemblyName)
                    .MigrationsHistoryTable("__EFMigrationHistory", PeopleContext.DEFAULT_SCHEMA);
            });

        return new PeopleContext(optionsBuilder.Options, null!);
    }
}
