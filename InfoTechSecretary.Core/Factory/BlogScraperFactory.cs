using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Database.Values;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTechSecretary.Core.Factory;

public class BlogScraperFactory(IServiceProvider serviceProvider) : IBlogScraperFactory
{
    public IBlogScraper GetScraper(BlogProvider provider)
    {
        return serviceProvider.GetRequiredKeyedService<IBlogScraper>(provider);
    }
}
