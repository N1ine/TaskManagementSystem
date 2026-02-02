using FluentValidation;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Api.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email format is incorrect.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(8).WithMessage("Password must contain more than 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase character.")
            .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase character.")
            .Matches("[0-9]").WithMessage("Password must contain at least 1 digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character.");
    }
}
