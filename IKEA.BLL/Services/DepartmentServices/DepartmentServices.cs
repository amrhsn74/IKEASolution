using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTOs.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistence.Repositories.Departments;
using IKEA.DAL.Presistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository Repository;
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments(bool WithNoTracking = true)
        {
            var Departments = unitOfWork.DepartmentRepository.GetAll().Select(dept => new DepartmentDto() 
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
            });

            return await Departments.ToListAsync();

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
        public async Task<DepartmentDetailsDto?> GetDepartmentById(int id)
        {
            var Department = await unitOfWork.DepartmentRepository.GetById(id);
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
        public async Task<int> CreatedDepartment(CreatedDepartmentDto departmentDto)
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
            unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return await unitOfWork.SaveChanges();
        }
        public async Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto)
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
            unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return await unitOfWork.SaveChanges(); 
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            var Department = await unitOfWork.DepartmentRepository.GetById(id);
            if (Department is not null) return false;
                //return Repository.Delete(Department)>0;
            else
            {
                unitOfWork.DepartmentRepository.Delete(Department);
                int result = await unitOfWork.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
        }

    }
}
