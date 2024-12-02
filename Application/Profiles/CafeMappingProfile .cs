using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class CafeMappingProfile : Profile
    {
        public CafeMappingProfile()
        {
             CreateMap<Cafe, CafeDto>()
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.EmployeeCafes.Count));

        CreateMap<CafeDto, Cafe>()
            .ForMember(dest => dest.EmployeeCafes, opt => opt.Ignore()); // Handle `EmployeeCafes` separately
        }
    }
}