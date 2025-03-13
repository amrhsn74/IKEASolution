using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.Presistence.Repositories.Departments
{
    /* Must implement 5 methods: 1.GetAll  2.GetById  3.Add  4.Update  5.Delete  */
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WithNoTracking = true);
        Department? GetById(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);

    }
}
