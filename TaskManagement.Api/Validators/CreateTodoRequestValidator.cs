using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Api.Validators;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(100).WithMessage("Title is too long (Max Length: 100).");
        
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description is too long (Max Length: 500).");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Incorrect priority value.");
        
        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in future.");
    }
}
