namespace vassilyev.EduCheckV2App.WebAPI.Entities;

public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;

    public Guid TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    public List<AnswerOption> Options { get; set; } = new();
}
