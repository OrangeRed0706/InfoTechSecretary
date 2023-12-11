using InfoTechSecretary.Core.Model;
using Post = InfoTechSecretary.Database.Entities.Post;
using Tag = InfoTechSecretary.Database.Entities.Tag;

namespace InfoTechSecretary.Core.Mapper;

public static class Mapper
{
    public static Post MapToBlogPost(this Model.Post post, BlogInfo blogInfo)
    {
        return new Post
        {
            BlogId = blogInfo.BlogId,
            Provider = blogInfo.Provider,
            Title = post.Title,
            Time = post.Time,
            Description = post.Description,
            Link = post.Link,
            CreatedTime = DateTimeOffset.UtcNow,
            UpdatedTime = DateTimeOffset.UtcNow,
            Tags = post.Tags,
        };
    }
}
