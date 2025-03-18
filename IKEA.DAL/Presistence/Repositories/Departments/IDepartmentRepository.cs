using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistence.Repositories._Generics;

namespace IKEA.DAL.Presistence.Repositories.Departments
{
    /* Must implement 5 methods: 1.GetAll  2.GetById  3.Add  4.Update  5.Delete   ==>  IGnericRepository  */
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // Signatures of DepartmentRepository methods only.
    }
}
