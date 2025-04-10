using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.Presistence.Repositories._Generics
{
    /* Must implement 5 methods: 1.GetAll  2.GetById  3.Add  4.Update  5.Delete  */
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetAll(bool WithNoTracking = true);
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
