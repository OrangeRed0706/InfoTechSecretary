using InfoTechSecretary.Core.Model;

namespace InfoTechSecretary.Core.Interfaces;

public interface INotificationService
{
    Task NotifyAsync((IEnumerable<Post> newPostsToSave, BlogInfo blogInfo) changedPost, CancellationToken stoppingToken = default);
}
