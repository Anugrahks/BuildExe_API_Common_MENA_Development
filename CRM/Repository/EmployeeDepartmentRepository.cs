using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Repository
{
    public class EmployeeDepartmentRepository : IEmployeeDepartmentRepository
    {
        private readonly ProductContext _dbContext;
        public EmployeeDepartmentRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete_EmployeeDepartment(int employeeDepartmentID)
        {
            var employeeDepartment = _dbContext.tbl_EmployeeDepartment.Find(employeeDepartmentID);

            _dbContext.tbl_EmployeeDepartment.Remove(employeeDepartment);
            Save();
        }

        public EmployeeDepartment GetEmployeeDepartmentByID(int employeeDepartmentID)
        {
            return _dbContext.tbl_EmployeeDepartment.Find(employeeDepartmentID);
        }

        public IEnumerable<EmployeeDepartment> GetEmployeeDepartment()
        {
            return _dbContext.tbl_EmployeeDepartment.ToList();
        }

        public void InsertEmployeeDepartment(EmployeeDepartment employeeDepartment)
        {
            _dbContext.Add(employeeDepartment);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update_EmployeeDepartment(EmployeeDepartment employeeDepartment)
        {
            _dbContext.Entry(employeeDepartment).State = EntityState.Modified;
            Save();
        }
    }
}
