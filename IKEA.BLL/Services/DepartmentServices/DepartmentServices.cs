using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTOs.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistence.Repositories.Departments;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private IDepartmentRepository Repository;
        public DepartmentServices(IDepartmentRepository repository)
        {
            Repository = repository;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments(bool WithNoTracking = true)
        {
            var Departments = Repository.GetAll().Select(dept => new DepartmentDto() 
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
            }).ToList();

            return Departments;

            //List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();
            //foreach (var Department in Departments)
            //{
            //    DepartmentDtos.Add(new DepartmentDto
            //    {
            //        Id = Department.Id,
            //        Name = Department.Name,
            //        Code = Department.Code,
            //        CreationDate = Department.CreationDate
            //    });
            //}

            //return DepartmentDtos;
        }
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = Repository.GetById(id);
            if (Department == null)
            {
                return null;
            }
            return new DepartmentDetailsDto
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate,
                CreatedBy = Department.CreatedBy,
                CreatedOn = Department.CreatedOn,
                LastModifiedBy = Department.LastModifiedBy,
                LastModifiedOn = Department.LastModifiedOn,
                IsDeleted = Department.IsDeleted
            };
        }

        public int CreatedDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Department
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };

            return Repository.Add(CreatedDepartment);
        }
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var UpdatedDepartment = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return Repository.Update(UpdatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var Department = Repository.GetById(id);
           //int result=0;
            if (Department is not null)
                return Repository.Delete(Department)>0;
            else
                return false;

        }

    }
}
