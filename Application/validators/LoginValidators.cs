using FluentValidation;
using MyApi.Application.DTOs;

namespace MyApi.Application.Validators;

public class LoginValidator : AbstractValidator<LoginInput>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}