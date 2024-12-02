using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
   public class EmployeeCafeMappingProfile : Profile
    {
        public EmployeeCafeMappingProfile()
        {
            CreateMap<EmployeeCafe, EmployeeCafeDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.CafeName, opt => opt.MapFrom(src => src.Cafe.Name))
                .ForMember(dest => dest.DaysWorked, opt => opt.MapFrom(src => (int)(DateTime.UtcNow - src.StartDate).TotalDays));

            CreateMap<EmployeeCafeDto, EmployeeCafe>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Cafe, opt => opt.Ignore());
        }
    }
}