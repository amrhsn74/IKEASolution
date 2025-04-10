using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IKEA.BLL.Common.Services.Attachements;
using IKEA.BLL.DTOs.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistence.Repositories.Employees;
using IKEA.DAL.Presistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAttachementServices attachementServices;

        public EmployeeServices(IUnitOfWork _unitOfWork, IMapper _mapper, IAttachementServices _attachementServices)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            attachementServices = _attachementServices;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search, bool WithNoTracking = true)
        {
            var Employees = unitOfWork.EmployeeRepository.GetAll();
            var FilteredEmployees = Employees.Where(E => E.IsDeleted == false && ( string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()) ) );
            var AfterFilteration = FilteredEmployees.Include(E => E.Department).Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType,
                Department = E.Department.Name ?? "N/A"
            }
            );
            return await AfterFilteration.ToListAsync();
        }
        public async Task<EmployeeDetailsDto?> GetEmployeeById(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department = employee.Department?.Name ?? "N/A",
                    ImageName = employee.ImageName,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                };
            }
            return null;
        }
        public async Task<int> CreatedEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedOn = DateTime.Now,
            };
            if (employeeDto.Image is not null)
            {
                employee.ImageName = attachementServices.UploadImage(employeeDto.Image,"images");
            }
            unitOfWork.EmployeeRepository.Add(employee);
            return await unitOfWork.SaveChanges();
        }
        public async Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                ImageName = employeeDto.ImageName,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            if (employeeDto.Image is not null)
            {
                if(employee.ImageName is not null)
                {
                    // Delete the old image file if it exists
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files", "images", employee.ImageName);
                    attachementServices.DeleteImage(oldFilePath);
                }
                employee.ImageName = attachementServices.UploadImage(employeeDto.Image, "images");
            }
            //employee.ImageName = attachementServices.UploadImage(employeeDto.Image, "images");
            unitOfWork.EmployeeRepository.Update(employee);
            return await unitOfWork.SaveChanges();
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            var Employee = await unitOfWork.EmployeeRepository.GetById(id);
            //int result=0;
            if (Employee is not null) 
            {
                if (Employee.ImageName is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files", "images", Employee.ImageName);
                    attachementServices.DeleteImage(filePath);
                }
                unitOfWork.EmployeeRepository.Delete(Employee);
                return await unitOfWork.SaveChanges() > 0 ? true : false;
            }
            else
            { 
                Employee.IsDeleted = true;
                unitOfWork.EmployeeRepository.Update(Employee);
                return await unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
