using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistence.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DepartmentRepository(ApplicationDbContext Context)
        {
            dbContext = Context;
        }
        public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            {
                return dbContext.Departments.AsNoTracking().ToList();
            }
            return dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            var Department = dbContext.Departments.Find(id);
            return Department;
        }
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }
        public int Delete(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }
    }
}
