using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Queries
{
    public class GetCafesQuery : IRequest<IEnumerable<CafeDto>>
    {
        public string? Location { get; set; }
    }

    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, IEnumerable<CafeDto>>
    {
    
            private readonly StoreContext _storeContext;

            private readonly IMapper _mapper;

            public GetCafesQueryHandler(StoreContext context, IMapper mapper)
            {
                _mapper = mapper;
                _storeContext = context;
            }

            public async Task<IEnumerable<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
            {
                 /* var cafes = await _repository.GetAllAsync();

                if (!string.IsNullOrEmpty(request.Location))
                    cafes = cafes.Where(c => c.Location == request.Location);

                return cafes.OrderByDescending(c => c.Employees.Count)
                            .Select(c => new CafeDto
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Description = c.Description,
                                Location = c.Location,
                                Employees = c.Employees.Count
                                //Logo = c.Logo != null ? Convert.ToBase64String(c.Logo) : null
                            }); */

                var cafes = await GetCafesByLocation(request.Location);
                return cafes;
                
            }

            public async Task<List<CafeDto>> GetCafesByLocation(string? location)
            {
                var query = _storeContext.Cafes
                    .Include(c => c.EmployeeCafes)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(location))
                {
                    query = query.Where(c => c.Location == location);
                }

                var cafes = await query
                    .OrderByDescending(c => c.EmployeeCafes.Count)
                    .Select(c => new CafeDto
                    {
                        Name = c.Name,
                        Description = c.Description,
                        Employees = c.EmployeeCafes.Count,
                        Logo = c.Logo,
                        Location = c.Location,
                        Id = c.Id
                    })
                    .ToListAsync();

                return cafes;
            }
    }

}