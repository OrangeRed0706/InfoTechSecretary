using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Core.Model;

public class BlogInfo
{
    public int BlogId { get; set; }
    public BlogProvider Provider { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
