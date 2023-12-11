using InfoTechSecretary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTechSecretary.Database.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable(nameof(Tag));

        builder.HasKey(b => b.TagId);

        builder.HasIndex(b => b.Name)
            .IsUnique()
            .HasDatabaseName("IX_Tags_Name");

        builder
            .Property(b => b.TagId)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder
            .Property(b => b.Name)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder
            .Property(b => b.CreatedTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder
            .Property(b => b.UpdatedTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();
    }
}
