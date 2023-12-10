using InfoTechSecretary.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTechSecretary.Database;

public class InfoTechSecretaryContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public InfoTechSecretaryContext() { }

    public InfoTechSecretaryContext(DbContextOptions<InfoTechSecretaryContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Database=InfoTechSecretary;Username=postgres;Password=1234";
        optionsBuilder.UseNpgsql(connectionString, options => { options.EnableRetryOnFailure(); });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfoTechSecretaryContext).Assembly);

        modelBuilder.HasSequence<int>("blog_id_seq")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.HasSequence<int>("post_id_seq")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.HasSequence<int>("tag_id_seq")
            .StartsAt(1)
            .IncrementsBy(1);
    }
}
