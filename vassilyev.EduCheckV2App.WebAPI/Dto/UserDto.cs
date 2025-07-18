using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    // Пароль не включаем в DTO для безопасности!
    public List<Guid> SessionIds { get; set; } = new();
}