using InfoTechSecretary.Core.Model;

namespace InfoTechSecretary.Core.Mapper;

public static class Mapper
{
    public static Database.Entities.Post MapToBlogPost(this Post post, BlogInfo blogInfo)
    {
        return new Database.Entities.Post
        {
            BlogId = blogInfo.BlogId,
            Provider = blogInfo.Provider,
            Title = post.Title,
            Time = post.Time,
            Description = post.Description,
            Link = post.Link,
            CreatedTime = DateTimeOffset.UtcNow,
            UpdatedTime = DateTimeOffset.UtcNow,
            Tags = post.Tags.Select(y => new Database.Entities.Tag
            {
                Name = y.Name,
                CreatedTime = DateTimeOffset.UtcNow,
                UpdatedTime = DateTimeOffset.UtcNow
            }).ToList(),
        };
    }
}
