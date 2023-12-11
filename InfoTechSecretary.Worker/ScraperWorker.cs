using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Model;
using InfoTechSecretary.Core.Services;
using InfoTechSecretary.Database.Entities;
using Post = InfoTechSecretary.Core.Model.Post;

namespace InfoTechSecretary.Worker;

public class ScraperWorker(
    ILogger<ScraperWorker> logger,
    IScraperService scraperService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessBlogsAsync(stoppingToken);
        }
    }

    private async Task ProcessBlogsAsync(CancellationToken stoppingToken)
    {
        var scraperBlogList = await scraperService.GetScraperBlogListAsync(stoppingToken);
        foreach (var scraperBlog in scraperBlogList)
        {
            await scraperService.ProcessBlogAsync(scraperBlog, stoppingToken);
        }
    }
}