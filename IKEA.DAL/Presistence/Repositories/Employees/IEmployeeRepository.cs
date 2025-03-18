using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistence.Repositories._Generics;

namespace IKEA.DAL.Presistence.Repositories.Employees
{
    /* Must implement 5 methods: 1.GetAll  2.GetById  3.Add  4.Update  5.Delete   ==>  IGenericRepository  */
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // Signatures of EmployeeRepository methods only.
    }
}
