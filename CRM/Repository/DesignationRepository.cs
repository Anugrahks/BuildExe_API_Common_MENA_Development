using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;

namespace BuildExeServices.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly ProductContext _dbContext;

        public DesignationRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteDesignation(int designationId)
        {
            var designation = _dbContext.tbl_EmployeeDesignation.Find(designationId);

            _dbContext.tbl_EmployeeDesignation.Remove(designation);
            Save();
        }

        public EmployeeDesignation GetDesignationByID(int designationId)
        {
            return _dbContext.tbl_EmployeeDesignation.Find(designationId);
        }

        public IEnumerable<EmployeeDesignation> GetDesignation()
        {
            return _dbContext.tbl_EmployeeDesignation.ToList();
        }

        public void InsertDesignation(EmployeeDesignation designation)
        {
            _dbContext.Add(designation);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateDesignation(EmployeeDesignation designation)
        {
            _dbContext.Entry(designation).State = EntityState.Modified;
            Save();
        }
    }
}
