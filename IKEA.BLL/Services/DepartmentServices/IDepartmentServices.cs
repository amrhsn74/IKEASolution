using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTOs.Departments;

namespace IKEA.BLL.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartments(bool WithNoTracking = true);
        Task<DepartmentDetailsDto?> GetDepartmentById(int id);
        Task<int> CreatedDepartment(CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto);
        Task<bool> DeleteDepartment(int id);
    }
}
