using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistence.Data;
using IKEA.DAL.Presistence.Repositories._Generics;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistence.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeRepository(ApplicationDbContext Context) : base(Context)
        {
            dbContext = Context;
        }
        
    }
}
