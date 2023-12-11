using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Mapper;
using InfoTechSecretary.Core.Model;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Core.Services;

public class ScraperService(IBlogScraperFactory blogScraperFactory, IBlogPostRepository blogPostRepository) : IScraperService
{
    public async Task<IEnumerable<BlogInfo>> GetScraperBlogListAsync(CancellationToken cancellationToken)
    {
        //暫時先HardCode新增SeedData
        await blogPostRepository.AddBlog(new BlogInfo
        {
            Provider = BlogProvider.Cloudflare,
            Name = "Cloudflare",
            Url = "https://blog.cloudflare.com/",
        }, cancellationToken);

        var blog = await blogPostRepository.GetScraperBlogListAsync(cancellationToken);
        return blog.Select(x => new BlogInfo
        {
            BlogId = x.BlogId,
            Provider = x.Provider,
            Name = x.Name,
            Url = x.Url,
        });
    }

    public IBlogScraper GetScraperBlogListAsync(BlogProvider provider)
    {
        return blogScraperFactory.GetScraper(provider);
    }

    public async Task<(IEnumerable<Post> newPostsToSave, BlogInfo blogInfo)> ProcessBlogAsync(BlogInfo blogInfo, CancellationToken stoppingToken)
    {
        var newPosts = await GetScrapePostsAsync(blogInfo, stoppingToken);
        var existingPosts = await blogPostRepository.GetExistingPostsAsync(blogInfo.Provider, 0, 100, stoppingToken);
        var existPostDic = existingPosts.ToDictionary(x => x.Link);
        var newPostsToSave = newPosts.Where(x => !existPostDic.ContainsKey(x.Link!)).ToList();

        if (newPostsToSave.Any())
        {
            await blogPostRepository.AddNewPostsAsync(newPostsToSave.Select(x => x.MapToBlogPost(blogInfo)));
            return (newPostsToSave, blogInfo);
        }
        else
        {
            return (Enumerable.Empty<Post>(), blogInfo);
        }
    }

    private Task<IEnumerable<Post>> GetScrapePostsAsync(BlogInfo blogInfo, CancellationToken cancellationToken = default)
    {
        var scraper = blogScraperFactory.GetScraper(blogInfo.Provider);
        return scraper.ScrapeBlogPostsAsync(blogInfo, cancellationToken);
    }
}
