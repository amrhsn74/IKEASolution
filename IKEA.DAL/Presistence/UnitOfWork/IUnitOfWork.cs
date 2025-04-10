using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Presistence.Repositories.Departments;
using IKEA.DAL.Presistence.Repositories.Employees;

namespace IKEA.DAL.Presistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public Task<int> SaveChanges();
    }
}
