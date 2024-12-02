using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Commands
{
   public class CreateCafeCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string Location { get; set; } = string.Empty;
    }

    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
         private readonly StoreContext _storeContext;

        public CreateCafeCommandHandler(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Domain.Entities.Cafe
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,
                Location = request.Location
            };

            _storeContext.Cafes.Add(cafe);
            await _storeContext.SaveChangesAsync(cancellationToken);

            return cafe.Id;
        }
    }

}