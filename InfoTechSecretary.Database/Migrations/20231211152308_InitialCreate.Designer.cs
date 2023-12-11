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
    [Migration("20231211152308_InitialCreate")]
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

                    b.Property<DateTimeOffset?>("CreatedTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Provider")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

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

                    b.Property<DateTimeOffset?>("CreatedTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<int>("Provider")
                        .HasColumnType("integer");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.HasIndex("Provider")
                        .IsUnique()
                        .HasDatabaseName("IX_Posts_Provider");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("TagId"));

                    b.Property<DateTimeOffset?>("CreatedTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("TagId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Tags_Name");

                    b.ToTable("Tag", (string)null);
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

            modelBuilder.Entity("InfoTechSecretary.Database.Entities.Blog", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
