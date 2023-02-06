using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.Entities;

namespace People.Infrastructure.Persistence.Context.EntitiesConfigurations;

public class CityEntityConfiguration : IEntityTypeConfiguration<City>
{
    private const string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("TB_Cities");
        builder.Property<int>(_keyPropertyName);
        builder.HasKey(_keyPropertyName);
    }
}
