namespace vassilyev.EduCheckV2App.WebAPI.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public List<UserSession> Sessions { get; set; } = new();
}
