using FluentValidation;
using MyApi.Application.DTOs;

namespace MyApi.Application.Validators;


public class CreateUserValidator : AbstractValidator<CreateUserInput>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
                            .WithMessage("Name is required.").MinimumLength(2)
                            .WithMessage("Name must be at least 2 characters.");

        RuleFor(x => x.Email).NotEmpty()
                            .WithMessage("Email is required.")
                            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password).NotEmpty()
                                .WithMessage("Password is required.")
                                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Phone).Matches(@"^\+?[1-9]\d{9}$")
                            .When(x => !string.IsNullOrEmpty(x.Phone))
                            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.Address).MaximumLength(200)
                            .WithMessage("Address cannot exceed 200 characters.");

    }
}