using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Services;

namespace InfoTechSecretary.Worker;

public class ScraperWorker(
    ILogger<ScraperWorker> logger,
    IScraperService scraperService,
    INotificationService notificationService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessBlogsAsync(stoppingToken);
            break;
        }
    }

    private async Task ProcessBlogsAsync(CancellationToken stoppingToken)
    {
        var scraperBlogList = await scraperService.GetScraperBlogListAsync(stoppingToken);
        foreach (var scraperBlog in scraperBlogList)
        {
            var result = await scraperService.ProcessBlogAsync(scraperBlog, stoppingToken);
            await notificationService.NotifyAsync(result, stoppingToken);
        }
    }
}
