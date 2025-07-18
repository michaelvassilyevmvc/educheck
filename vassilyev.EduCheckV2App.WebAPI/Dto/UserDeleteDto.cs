namespace vassilyev.EduCheckV2App.WebAPI.Dto;

public class UserDeleteDto
{
    public Guid Id { get; set; }
    public string? VerificationPassword { get; set; } // Подтверждение пароля для удаления
}