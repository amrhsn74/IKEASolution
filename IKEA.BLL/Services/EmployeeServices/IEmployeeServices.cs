using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.DTOs.Employees;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search, bool WithNoTracking = true);
        Task<EmployeeDetailsDto?> GetEmployeeById(int id);
        Task<int> CreatedEmployee(CreatedEmployeeDto employeeDto);
        Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto);
        Task<bool> DeleteEmployee(int id);
    }
}
