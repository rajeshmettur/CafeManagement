using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly StoreContext _context;

        public UpdateEmployeeCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);
            if (employee == null) return false;

            employee.Name = request.Name;
            employee.EmailAddress = request.EmailAddress;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Gender = request.Gender;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}