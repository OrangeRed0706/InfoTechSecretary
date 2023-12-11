using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Core.Interfaces;

public interface IBlogScraperFactory
{
    IBlogScraper GetScraper(BlogProvider provider);
}
