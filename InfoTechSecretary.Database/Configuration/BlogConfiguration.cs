using InfoTechSecretary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTechSecretary.Database.Configuration;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable(nameof(Blog));

        builder.HasKey(b => b.BlogId);

        builder
            .Property(b => b.BlogId)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder
            .HasIndex(b => b.Provider)
            .IsUnique()
            .HasDatabaseName("IX_Blogs_Provider");

        builder
            .Property(b => b.Provider)
            .HasColumnType("integer")
            .IsRequired();

        builder
            .Property(b => b.Name)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder
            .Property(b => b.Url)
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

        builder
            .HasMany(b => b.Posts)
            .WithOne(p => p.Blog)
            .HasForeignKey(p => p.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
