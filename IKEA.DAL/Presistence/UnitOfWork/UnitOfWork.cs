using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Presistence.Data;
using IKEA.DAL.Presistence.Repositories.Departments;
using IKEA.DAL.Presistence.Repositories.Employees;

namespace IKEA.DAL.Presistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        public UnitOfWork(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, ApplicationDbContext dbContext)
        {
            _departmentRepository = new Lazy<IDepartmentRepository>(valueFactory:() => new DepartmentRepository(dbContext)) ;
            _employeeRepository = new Lazy<IEmployeeRepository>(valueFactory:() => new EmployeeRepository(dbContext));
            this.dbContext = dbContext;
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public async Task<int> SaveChanges() => await dbContext.SaveChangesAsync();
    }

}
