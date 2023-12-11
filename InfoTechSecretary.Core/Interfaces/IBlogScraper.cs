using InfoTechSecretary.Core.Model;

namespace InfoTechSecretary.Core.Interfaces;

public interface IBlogScraper
{
    Task<IEnumerable<Post>> ScrapeBlogPostsAsync(BlogInfo blogInfo, CancellationToken cancellationToken = default);
}
