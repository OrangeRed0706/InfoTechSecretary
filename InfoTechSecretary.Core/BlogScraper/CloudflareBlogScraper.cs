using AngleSharp.Html.Parser;
using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Model;

namespace InfoTechSecretary.Core.BlogScraper;

public class CloudflareBlogScraper(IHttpClientFactory httpClientFactory) : IBlogScraper
{
    public async Task<IEnumerable<Post>> ScrapeBlogPostsAsync(BlogInfo blogInfo, CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient();
        var html = await httpClient.GetStringAsync(blogInfo.Url, cancellationToken);
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(html);

        var posts = document.QuerySelectorAll("article");
        var blogPosts = new List<Post>();

        foreach (var post in posts)
        {
            var title = post.QuerySelector("h2")?.TextContent;
            var time = post.QuerySelector("p.f3.fw5.gray5.my")?.TextContent.Trim();
            var description = post.QuerySelector("p.f4.lh-copy.fw3, p.f3.lh-copy.fw4.gray1")?.TextContent.Trim();
            var link = post.QuerySelector("a")?.GetAttribute("href");
            var tags = post.QuerySelectorAll("a.dib").Select(tag => tag.TextContent.Trim()).ToList();

            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Time: {time}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"Link: {blogInfo.Url}{link}");
            Console.WriteLine($"Tags: {string.Join(", ", tags)}");

            blogPosts.Add(new Post
            {
                Title = title,
                Time = DateTimeOffset.Parse(time).ToUniversalTime(),
                Description = description,
                Link = $"{blogInfo}/{link}",
                Tags = tags.Select(tag => new Tag { Name = tag }).ToList(),
            });
        }

        return blogPosts;
    }
}
