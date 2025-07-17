namespace vassilyev.EduCheckV2App.WebAPI.Entities;

public class AnswerOption
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
