using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.Domain.Entities;

namespace People.Infrastructure.Persistence.Context.EntitiesConfigurations;

public class PhoneNumberEntityConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    private const string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("TB_PhoneNumbers");
        builder.Property<int>(_keyPropertyName);
        builder.HasKey(_keyPropertyName);
    }
}
