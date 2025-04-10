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
        public async Task<T?> GetById(int id)
        {
            var Entity = await dbContext.Set<T>().FindAsync(id);
            return Entity;
        }
        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            dbContext.Set<T>().Remove(entity);
        }
    }
}
