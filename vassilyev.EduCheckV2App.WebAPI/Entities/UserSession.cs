namespace vassilyev.EduCheckV2App.WebAPI.Entities;

public class UserSession
{
    public Guid Id { get; set; }
    public DateTime StartedAt { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    public SessionStats Stats { get; set; } = new();
}
