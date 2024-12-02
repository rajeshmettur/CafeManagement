using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Commands
{
    public class UpdateCafeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string Location { get; set; } = string.Empty;
    }

    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, bool>
    {
        private readonly StoreContext _storeContext;

        public UpdateCafeCommandHandler(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<bool> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _storeContext.Cafes.FindAsync(request.Id);
            if (cafe == null) return false;

            // Update fields
            cafe.Name = request.Name;
            cafe.Description = request.Description;
            cafe.Logo = request.Logo;
            cafe.Location = request.Location;

            await _storeContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}