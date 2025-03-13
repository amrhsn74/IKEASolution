using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Presistence.Repositories.Departments;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private IDepartmentRepository Repository;
        public DepartmentServices(IDepartmentRepository repository)
        {
            Repository = repository;
        }
    }
}
