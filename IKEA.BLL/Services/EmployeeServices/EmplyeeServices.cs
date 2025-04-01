using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTOs.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistence.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmplyeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmplyeeServices(IEmployeeRepository Repository)
        {
            _employeeRepository = Repository;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithNoTracking = true)
        {
            var Employees = _employeeRepository.GetAll();
            var FilteredEmployees = Employees.Where(E => E.IsDeleted == false);
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
            return AfterFilteration.ToList();
        }
        public EmployeeDetailsDto GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
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
                    Department = employee.Department.Name ?? "N/A",
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                };
            }
            return null;
        }
        public int CreatedEmployee(CreatedEmployeeDto employeeDto)
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
            return _employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
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
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return _employeeRepository.Update(employee);
        }
        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            //int result=0;
            if (Employee is not null)
                return _employeeRepository.Delete(Employee) > 0;
            else
                return false;
        }
    }
}
