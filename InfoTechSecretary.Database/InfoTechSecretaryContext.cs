using InfoTechSecretary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfoTechSecretary.Database;

public class InfoTechSecretaryContext : DbContext
{
    public InfoTechSecretaryContext() { }

    public InfoTechSecretaryContext(DbContextOptions<InfoTechSecretaryContext> options) : base(options) { }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("InfoTechSecretaryContext"), options => { options.EnableRetryOnFailure(); });
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
