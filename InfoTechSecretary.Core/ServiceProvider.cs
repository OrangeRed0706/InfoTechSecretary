using InfoTechSecretary.Core.BlogScraper;
using InfoTechSecretary.Core.Factory;
using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Repository;
using InfoTechSecretary.Core.Services;
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
        services.AddSingleton<IBlogPostRepository, BlogPostRepository>();
        services.AddSingleton<INotificationService, DiscordNotificationService>();
        services.AddSingleton(new TranslatorService(configuration.GetSection("OpenAiKey").Get<string>()));
        services.AddScraperServices();
    }

    private static void AddScraperServices(this IServiceCollection services)
    {
        services.AddSingleton<IBlogScraperFactory, BlogScraperFactory>();
        services.AddSingleton<IScraperService, ScraperService>();
        services.AddKeyedSingleton<IBlogScraper, CloudflareBlogScraper>(BlogProvider.Cloudflare);
    }
}
