using Discord;
using Discord.Webhook;
using InfoTechSecretary.Core.Interfaces;
using InfoTechSecretary.Core.Model;

namespace InfoTechSecretary.Core.Services;

public class DiscordNotificationService(TranslatorService translatorService) : INotificationService
{
    public async Task NotifyAsync((IEnumerable<Post> newPostsToSave, BlogInfo blogInfo) dto, CancellationToken stoppingToken)
    {
        var client = new DiscordWebhookClient("https://discord.com/api/webhooks/1183819139232583702/KD6-ljZ84N43Qyy58chvWAxT9SXT31zRf9BJR0qT0ScAdwEB1w2h4e23oARIgjzJx5vS");
        var blogInfo = dto.blogInfo;
        foreach (var post in dto.newPostsToSave)
        {
            var translatedString = await translatorService.TranslateAndImproveAsync(post.Description);
            var embed = new EmbedBuilder
            {
                Title = post.Title,
                Description = $"{post.Description} \n {translatedString}",
                Url = post.Link,
                ThumbnailUrl = null,
                ImageUrl = null,
                Fields = new List<EmbedFieldBuilder>(),
                Timestamp = DateTimeOffset.Now,
                Color = GetRandomColor(),
                Author = new EmbedAuthorBuilder
                {
                    Name = blogInfo.Provider.ToString(),
                    Url = blogInfo.Url,
                    IconUrl = blogInfo.IconUrl
                },
                Footer = new EmbedFooterBuilder()
                {
                    Text = "InfoTechSecretary"
                }
            }.Build();

            await client.SendMessageAsync(embeds: new[] { embed });
            await Task.Delay(10, stoppingToken);
        }
    }

    private static Color GetRandomColor()
    {
        var random = new Random();
        return new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}
