using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.SeedData;

namespace Application.Mediator_Handlers.Employee.Queries.QueryValidator
{
    public class GetEmployeeByIdQuery: IRequest<IEnumerable<EmployeeDto>>
    {
         public Guid CafeId { get; set; }

    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, IEnumerable<EmployeeDto>>
    {
            private readonly StoreContext _storeContext;

            private readonly IMapper _mapper;

            public GetEmployeeByIdQueryHandler(StoreContext context, IMapper mapper)
            {
                _mapper = mapper;
                _storeContext = context;
            }

            public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                 var employees = await GetEmployeesByCafe(request.CafeId);
                //var employees = await GetEmployeesByCafe(request.CafeName);
                return employees;
            }

            public async Task<List<EmployeeDto>> GetEmployeesByCafe(Guid CafeId)
            {
                var query = _storeContext.EmployeeCafes
                    .Include(ec => ec.Employee)
                    .Include(ec => ec.Cafe)
                    .AsQueryable();

                //if (CafeId != null)
               // {
                    query = query.Where(ec => ec.CafeId == CafeId);
                //}

                
                var employees = await query
                    .Select(ec => new EmployeeDto
                    {
                        Id = ec.Employee.Id,
                        Name = ec.Employee.Name,
                        EmailAddress = ec.Employee.EmailAddress,
                        PhoneNumber = ec.Employee.PhoneNumber,
                        Gender = ec.Employee.Gender,
                        DaysWorked = (DateTime.Now - ec.StartDate).Days,
                        Cafe = ec.Cafe.Name,
                        CafeId = ec.CafeId
                    })
                    .OrderByDescending(e => e.Cafe)
                    .ToListAsync();

                return employees;
            }
    }
}
