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
            .ForPath(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code));





        }
    }
}
