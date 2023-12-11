using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Mapper;
using InfoTechSecretary.Core.Model;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Core.Services;

public interface IScraperService
{
    IBlogScraper GetScraperBlogListAsync(BlogProvider provider);

    Task<IEnumerable<BlogInfo>> GetScraperBlogListAsync(CancellationToken cancellationToken = default);

    Task ProcessBlogAsync(BlogInfo blogInfo, CancellationToken cancellationToken = default);
}

public class ScraperService(IBlogScraperFactory blogScraperFactory, IBlogPostRepository blogPostRepository) : IScraperService
{
    public async Task<IEnumerable<BlogInfo>> GetScraperBlogListAsync(CancellationToken cancellationToken)
    {
        var blog = await blogPostRepository.GetScraperBlogListAsync(cancellationToken);
        return blog.Select(x => new BlogInfo
        {
            BlogId = x.BlogId,
            Provider = x.Provider,
            Name = x.Name,
            Url = x.Url
        });
    }

    public IBlogScraper GetScraperBlogListAsync(BlogProvider provider)
        => blogScraperFactory.GetScraper(provider);

    public async Task ProcessBlogAsync(BlogInfo blogInfo, CancellationToken stoppingToken)
    {
        var scraper = blogScraperFactory.GetScraper(blogInfo.Provider);
        var newPosts = await GetScrapePostsAsync(blogInfo, stoppingToken);
        var existingPosts = await blogPostRepository.GetExistingPostsAsync(blogInfo.Provider, 0, 100, stoppingToken);
        var existPostDic = existingPosts.ToDictionary(x => x.Link);
        var newPostsToSave = newPosts.Where(x => !existPostDic.ContainsKey(x.Link!));
        await blogPostRepository.AddNewPostsAsync(newPostsToSave.Select(x => x.MapToBlogPost(blogInfo)));
    }

    private Task<IEnumerable<Post>> GetScrapePostsAsync(BlogInfo blogInfo, CancellationToken cancellationToken = default)
    {
        var scraper = blogScraperFactory.GetScraper(blogInfo.Provider);
        return scraper.ScrapeBlogPostsAsync(blogInfo, cancellationToken);
    }
}
