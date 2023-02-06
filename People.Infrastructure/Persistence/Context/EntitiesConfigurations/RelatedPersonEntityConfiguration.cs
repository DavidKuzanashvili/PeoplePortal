using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.Entities;

namespace People.Infrastructure.Persistence.Context.EntitiesConfigurations;

internal class RelatedPersonEntityConfiguration : IEntityTypeConfiguration<RelatedPerson>
{
    private const string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<RelatedPerson> builder)
    {
        builder.ToTable("TB_RelatedPeople");
        builder.Property<int>(_keyPropertyName);
        builder.HasKey(_keyPropertyName);
    }
}
