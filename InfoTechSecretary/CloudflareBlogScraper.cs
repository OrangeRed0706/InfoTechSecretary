using AngleSharp.Html.Parser;
using InfoTechSecretary.Database.Entities;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary;

public class CloudflareBlogScraper
{
    public async Task<List<Post>> ScrapeBlogPostsAsync()
    {
        var httpClient = new HttpClient();
        const string baseUrl = "https://blog.cloudflare.com";
        var html = await httpClient.GetStringAsync(baseUrl);
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(html);

        var posts = document.QuerySelectorAll("article");
        var blogPosts = new List<Post>();

        foreach (var post in posts)
        {
            Console.WriteLine("-----------------------------------");
            var title = post.QuerySelector("h2")?.TextContent;
            var time = post.QuerySelector("p.f3.fw5.gray5.my")?.TextContent.Trim();
            var description = post.QuerySelector("p.f4.lh-copy.fw3, p.f3.lh-copy.fw4.gray1")?.TextContent.Trim();
            var link = post.QuerySelector("a")?.GetAttribute("href");
            var tags = new List<string>();
            foreach (var tag in post.QuerySelectorAll("a.dib"))
            {
                tags.Add(tag.TextContent.Trim());
            }

            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Time: {time}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"Link: {baseUrl}{link}");
            Console.WriteLine($"Tags: {string.Join(", ", tags)}");

            blogPosts.Add(new Post
            {
                Provider = BlogProvider.Cloudflare,
                Title = title,
                Time = DateTimeOffset.Parse(time).ToUniversalTime(),
                Description = description,
                Link = baseUrl + link,
                Tags = tags.Select(tag => new Tag { Name = tag }).ToList(),
            });
        }

        return blogPosts;
    }
}
