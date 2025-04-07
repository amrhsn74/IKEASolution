using AutoMapper;
using IKEA.BLL.DTOs.Departments;
using IKEA.PL.Models;

namespace IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedDepartmentDto, DepartmentVM>().ReverseMap();
            CreateMap<DepartmentDetailsDto, DepartmentVM>().ReverseMap();
            CreateMap<UpdatedDepartmentDto, DepartmentVM>().ReverseMap();
        }
    }    
}
