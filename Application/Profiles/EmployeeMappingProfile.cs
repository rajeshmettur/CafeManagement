using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
   public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, EmployeeDto>()
        .ForMember(dest => dest.Cafe, opt => opt.MapFrom(src => src.EmployeeCafes.Any() ? src.EmployeeCafes.First().Cafe.Name : string.Empty))
        .ForMember(dest => dest.CafeId, opt => opt.MapFrom(src => src.EmployeeCafes.Any() ? src.EmployeeCafes.First().CafeId : (Guid?)null))
        .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.EmployeeCafes.Any() ? src.EmployeeCafes.First().StartDate : (DateTime?)null))
        .ForMember(dest => dest.DaysWorked, opt => opt.MapFrom(src => src.EmployeeCafes.Any()
            ? (int)(DateTime.UtcNow - src.EmployeeCafes.First().StartDate).TotalDays
            : 0));
       CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.EmployeeCafes, opt => opt.Ignore()); // Handle `EmployeeCafes` separately in the logic
    }
}
}