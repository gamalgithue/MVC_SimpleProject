using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Mapper
{
  public class DomainProfile:Profile
    {
          public DomainProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<EmployeeDTO, Employee>().ReverseMap()
            .ForPath(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForPath(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code))
           .ForPath(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
            .ForPath(dest => dest.CityName, opt => opt.MapFrom(src => src.District.City.Name))
             .ForPath(dest => dest.CountryName, opt => opt.MapFrom(src => src.District.City.Country.Name));





            CreateMap<CountryDTO,Country>().ReverseMap();
            CreateMap<CityDTO,City>().ReverseMap();
             CreateMap<DistrictDTO,District>().ReverseMap();



        }
    }
}
