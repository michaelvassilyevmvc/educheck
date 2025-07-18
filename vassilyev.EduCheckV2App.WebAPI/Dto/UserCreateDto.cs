using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Dto;

public class UserCreateDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!; // Пароль в "чистом" виде
}