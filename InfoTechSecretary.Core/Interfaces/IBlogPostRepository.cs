using InfoTechSecretary.Core.Model;
using InfoTechSecretary.Database.Entities;
using InfoTechSecretary.Database.Values;
using Post = InfoTechSecretary.Database.Entities.Post;

namespace InfoTechSecretary.Core.Interfaces;

public interface IBlogPostRepository
{
    Task AddBlog(BlogInfo blogInfo, CancellationToken cancellationToken = default);

    Task<IEnumerable<Post>> GetExistingPostsAsync(BlogProvider provider, int skip, int take, CancellationToken cancellationToken = default);

    Task AddNewPostsAsync(IEnumerable<Post> newPosts);

    Task<IEnumerable<Blog>> GetScraperBlogListAsync(CancellationToken cancellationToken = default);
}
