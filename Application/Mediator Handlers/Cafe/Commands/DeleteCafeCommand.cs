using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Cafe.Commands
{
    public class DeleteCafeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, bool>
    {
        private readonly StoreContext _storecontext;

        public DeleteCafeCommandHandler(StoreContext storecontext)
        {
            _storecontext = storecontext;
        }

        public async Task<bool> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _storecontext.Cafes.FindAsync(request.Id);
            if (cafe == null) return false;

            _storecontext.Cafes.Remove(cafe);
            await _storecontext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}