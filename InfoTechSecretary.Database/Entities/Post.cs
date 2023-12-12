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
    public string IconUrl { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? UpdatedTime { get; set; }
    public string Tags { get; set; }
    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}
