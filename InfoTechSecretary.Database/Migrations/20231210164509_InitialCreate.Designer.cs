﻿// <auto-generated />
using System;
using InfoTechSecretary.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfoTechSecretary.Database.Migrations
{
    [DbContext(typeof(InfoTechSecretaryContext))]
    [Migration("20231210164509_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence<int>("blog_id_seq");

            modelBuilder.HasSequence<int>("post_id_seq");

            modelBuilder.HasSequence<int>("tag_id_seq");

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("BlogId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Provider")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("BlogId");

                    b.HasIndex("Provider")
                        .IsUnique()
                        .HasDatabaseName("IX_Blogs_Provider");

                    b.ToTable("Blog", (string)null);
                });

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("PostId"));

                    b.Property<int>("BlogId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<int>("Provider")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("TagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("TagId");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<int>("PostsPostId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("integer");

                    b.HasKey("PostsPostId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Post", b =>
                {
                    b.HasOne("InfoTechSecretary.Database.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("InfoTechSecretary.Database.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfoTechSecretary.Database.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Blog", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
