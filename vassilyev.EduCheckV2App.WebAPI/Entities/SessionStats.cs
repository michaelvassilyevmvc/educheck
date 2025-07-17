namespace EduCheck.Api.Entities;

public class SessionStats
{
    public Guid Id { get; set; }

    public int CorrectAnswers { get; set; }
    public int IncorrectAnswers { get; set; }

    public Guid UserSessionId { get; set; }
    public UserSession UserSession { get; set; } = null!;
}
