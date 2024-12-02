using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Employee.Commands
{
   public class DeleteEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }


    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly StoreContext _context;

        public DeleteEmployeeCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}