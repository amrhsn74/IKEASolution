﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistence.Data;
using IKEA.DAL.Presistence.Repositories._Generics;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DepartmentRepository(ApplicationDbContext Context): base(Context)
        {
            dbContext = Context;
        }
    }
}
