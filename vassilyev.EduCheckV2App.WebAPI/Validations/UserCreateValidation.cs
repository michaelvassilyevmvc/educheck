using FluentValidation;
using vassilyev.EduCheckV2App.WebAPI.Dto;

namespace vassilyev.EduCheckV2App.WebAPI.Validations;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required")
            .MinimumLength(3).WithMessage("Login must be at least 3 characters")
            .MaximumLength(50).WithMessage("Login must not exceed 50 characters")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Login can only contain letters, numbers and underscores");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
    }
}