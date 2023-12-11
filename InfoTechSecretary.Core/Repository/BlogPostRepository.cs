using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Model;
using InfoTechSecretary.Database;
using InfoTechSecretary.Database.Entities;
using InfoTechSecretary.Database.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Post = InfoTechSecretary.Database.Entities.Post;

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

    public async Task<IEnumerable<Blog>> GetScraperBlogListAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InfoTechSecretaryContext>();
        return await context.Blogs.ToListAsync(cancellationToken);
    }

    public async Task AddBlog(BlogInfo blogInfo, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InfoTechSecretaryContext>();
        var blog = context.Blogs.FirstOrDefault(x => x.Provider == blogInfo.Provider);
        if (blog != null)
        {
            return;
        }

        context.Blogs.Add(new Blog
        {
            Provider = blogInfo.Provider,
            Name = blogInfo.Name,
            Url = blogInfo.Url,
            CreatedTime = DateTimeOffset.UtcNow,
            UpdatedTime = DateTimeOffset.UtcNow,
            IsEnabled = true,
        });

        await context.SaveChangesAsync(cancellationToken);
    }
}
