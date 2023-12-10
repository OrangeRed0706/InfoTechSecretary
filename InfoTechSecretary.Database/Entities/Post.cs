using System.Collections.ObjectModel;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Database.Entities;

public class Post
{
    public int PostId { get; set; }
    public BlogProvider Provider { get; set; }
    public string Title { get; set; }
    public DateTimeOffset Time { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public virtual ICollection<Tag> Tags { get; set; } = new Collection<Tag>();

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}
