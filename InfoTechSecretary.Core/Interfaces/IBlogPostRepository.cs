using InfoTechSecretary.Database.Entities;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Core.Interfaces;

public interface IBlogPostRepository
{
    Task<IEnumerable<Post>> GetExistingPostsAsync(BlogProvider provider, int skip, int take, CancellationToken cancellationToken = default);

    Task AddNewPostsAsync(IEnumerable<Post> newPosts);

    Task<IEnumerable<Blog>> GetScraperBlogListAsync(CancellationToken cancellationToken = default);
}
