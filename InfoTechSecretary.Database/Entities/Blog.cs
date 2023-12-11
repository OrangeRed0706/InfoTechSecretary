using System.Collections.ObjectModel;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Database.Entities;

public class Blog
{
    public int BlogId { get; set; }
    public BlogProvider Provider { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? UpdatedTime { get; set; }

    public bool IsEnabled { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new Collection<Post>();
}
