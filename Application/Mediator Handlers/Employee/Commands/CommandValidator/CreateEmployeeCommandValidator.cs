using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mediator_Handlers.Commands;
using FluentValidation;

namespace Application.Mediator_Handlers.Employee.Commands.CommandValidator
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches("^[89][0-9]{7}$").WithMessage("Phone number must start with 8 or 9 and contain 8 digits.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female")
                .WithMessage("Gender must be either 'Male' or 'Female'.");
        }
    }
}