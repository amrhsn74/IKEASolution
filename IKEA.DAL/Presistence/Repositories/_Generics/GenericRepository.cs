using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistence.Repositories._Generics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {

        private readonly ApplicationDbContext dbContext;
        public GenericRepository(ApplicationDbContext Context)
        {
            dbContext = Context;
        }
        public IQueryable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return dbContext.Set<T>().AsNoTracking();
            }
            return dbContext.Set<T>();
        }
        public T? GetById(int id)
        {
            var Entity = dbContext.Set<T>().Find(id);
            return Entity;
        }
        public int Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }
    }
}
