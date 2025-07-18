using FluentValidation;
using vassilyev.EduCheckV2App.WebAPI.Dto;

namespace vassilyev.EduCheckV2App.WebAPI.Validations;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        When(x => !string.IsNullOrEmpty(x.NewLogin), () =>
        {
            RuleFor(x => x.NewLogin)
                .MinimumLength(3).WithMessage("Login must be at least 3 characters")
                .MaximumLength(50).WithMessage("Login must not exceed 50 characters");
        });

        When(x => !string.IsNullOrEmpty(x.NewPassword), () =>
        {
            RuleFor(x => x.NewPassword)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit");
        });
    }
}