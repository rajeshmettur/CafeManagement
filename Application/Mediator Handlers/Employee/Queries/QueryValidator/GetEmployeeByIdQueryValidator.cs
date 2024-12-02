using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Mediator_Handlers.Employee.Queries.QueryValidator
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            // RuleFor(x => x.Id)
            //     .NotEmpty().WithMessage("Employee ID is required.")
            //     .Matches("^UI[0-9A-Za-z]{7}$").WithMessage("Employee ID must follow the format 'UIXXXXXXX'.");
        }
    }
}