using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Database;
using InfoTechSecretary.Database.Entities;
using InfoTechSecretary.Database.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTechSecretary.Core.Repository;

public class BlogPostRepository(IServiceProvider serviceProvider) : IBlogPostRepository
{
    public async Task<IEnumerable<Post>> GetExistingPostsAsync(BlogProvider provider, int skip, int take, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InfoTechSecretaryContext>();
        return await context.Posts
            .Where(x => x.Provider == provider)
            .OrderByDescending(x => x.Time)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task AddNewPostsAsync(IEnumerable<Post> newPosts)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InfoTechSecretaryContext>();
        context.Posts.AddRange(newPosts);
        await context.SaveChangesAsync();
    }

    public Task<IEnumerable<Blog>> GetScraperBlogListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
