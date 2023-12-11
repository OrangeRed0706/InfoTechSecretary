using InfoTechSecretary.Core.BlogScraper;
using InfoTechSecretary.Core.Factory;
using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Repository;
using InfoTechSecretary.Database;
using InfoTechSecretary.Database.Values;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTechSecretary.Core;

public static class ServiceProvider
{
    public static void AddInfoTechSecretary(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfoTechSecretaryContext(configuration);
        services.AddSingleton<BlogPostRepository>();
        services.AddScraper();
    }

    private static void AddScraper(this IServiceCollection services)
    {
        services.AddSingleton<IBlogScraperFactory, BlogScraperFactory>();
        services.AddKeyedSingleton<IBlogScraper, CloudflareBlogScraper>(BlogProvider.Cloudflare);
    }
}
