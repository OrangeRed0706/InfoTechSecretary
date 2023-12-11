using System.Collections.ObjectModel;

namespace InfoTechSecretary.Database.Entities;

public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? UpdatedTime { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = new Collection<Post>();
}
