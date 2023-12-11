namespace InfoTechSecretary.Core.Model;

public class Post
{
    public string? Title { get; set; }
    public DateTimeOffset Time { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public string? Tags { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? UpdatedTime { get; set; }
}
