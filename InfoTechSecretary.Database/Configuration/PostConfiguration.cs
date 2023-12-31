﻿using InfoTechSecretary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTechSecretary.Database.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable(nameof(Post));

        builder.HasKey(b => b.PostId);

        builder
            .HasIndex(b => b.Provider)
            .HasDatabaseName("IX_Posts_Provider");

        builder
            .HasIndex(b => b.Link)
            .IsUnique()
            .HasDatabaseName("IX_Posts_Link");

        builder
            .Property(b => b.PostId)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder
            .Property(b => b.Provider)
            .HasColumnType("integer")
            .IsRequired();

        builder
            .Property(b => b.Title)
            .HasColumnType("varchar")
            .IsRequired();

        builder
            .Property(b => b.Time)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder
            .Property(b => b.Description)
            .HasColumnType("varchar")
            .IsRequired();

        builder
            .Property(b => b.TranslatedDescription)
            .HasColumnType("varchar")
            .IsRequired();

        builder
            .Property(b => b.Link)
            .HasColumnType("varchar")
            .IsRequired();

        builder
            .Property(b => b.IconUrl)
            .HasColumnType("varchar")
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
            .Property(b => b.Tags)
            .HasColumnType("varchar")
            .IsRequired();

        builder
            .Property(b => b.IsProcessed)
            .HasColumnType("boolean")
            .IsRequired();

        builder
            .Property(b => b.IsNotified)
            .HasColumnType("boolean")
            .IsRequired();

        builder
            .HasOne(b => b.Blog)
            .WithMany(p => p.Posts)
            .HasForeignKey(p => p.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
