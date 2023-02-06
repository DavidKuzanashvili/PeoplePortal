using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.Entities;

namespace People.Infrastructure.Persistence.Context.EntitiesConfigurations;

public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
{
    private const string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("TB_People");
        builder.Property<int>(_keyPropertyName);
        builder.HasKey(_keyPropertyName);

        builder
            .HasOne(x => x.City)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Metadata
            .FindNavigation(nameof(Person.PhoneNumbers))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.PhoneNumbers)
            .WithOne()
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(Person.RelatedPeople))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.RelatedPeople)
            .WithOne();
    }
}
