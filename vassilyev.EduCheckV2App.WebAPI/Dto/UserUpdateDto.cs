using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Dto;

public class UserUpdateDto
{
    public string? NewLogin { get; set; }
    public string? NewPassword { get; set; } // Опциональное обновление пароля
}