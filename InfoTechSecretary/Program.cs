using InfoTechSecretary;
using InfoTechSecretary.Database;
using InfoTechSecretary.Database.Entities;
using InfoTechSecretary.Database.Values;
using Microsoft.EntityFrameworkCore;

var context = new InfoTechSecretaryContext();

context.Database.EnsureCreated();

var scraper = new CloudflareBlogScraper();
var posts = await scraper.ScrapeBlogPostsAsync();

var blogger = context.Blogs.Include(blog => blog.Posts).First(x => x.Provider == posts.First().Provider);
blogger.Posts.Clear();
posts.ForEach(post => post.Blog = blogger);
context.Posts.AddRange(posts);
await context.SaveChangesAsync();
