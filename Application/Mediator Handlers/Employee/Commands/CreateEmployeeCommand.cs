using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Commands
{
    public class CreateEmployeeCommand : IRequest<string>
    {

        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly StoreContext _context;

        public CreateEmployeeCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee
            {
                Id = $"UI{Guid.NewGuid().ToString("N").Substring(0, 7).ToUpper()}",
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}