namespace vassilyev.EduCheckV2App.WebAPI.Dto;

public class TopicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Guid> QuestionIds { get; set; } = new();
}