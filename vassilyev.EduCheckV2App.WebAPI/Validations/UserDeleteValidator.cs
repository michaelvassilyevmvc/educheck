using FluentValidation;
using vassilyev.EduCheckV2App.WebAPI.Dto;

namespace vassilyev.EduCheckV2App.WebAPI.Validations;

public class UserDeleteValidation: AbstractValidator<UserDeleteDto>
{
    public UserDeleteValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.VerificationPassword)
            .NotEmpty()
            .MinimumLength(8);
    }
}