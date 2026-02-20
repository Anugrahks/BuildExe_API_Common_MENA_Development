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
    public class EmployeeMasterRepository :IEmployeeMasterRepository 
    {
        private readonly ProductContext _dbContext;

        public EmployeeMasterRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void InsertEmployee(Employee employeeMaster)
        {
            _dbContext.Add(employeeMaster);
            Save();
        }
        public void UpdateEmployee(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            Save();
        }
        public void DeleteEmployee(int employeeId)
        {
            var employee = _dbContext.tbl_EmployeeMaster.Find(employeeId);

            _dbContext.tbl_EmployeeMaster.Remove(employee);
            Save();
        }

        public IEnumerable<Employee> GetEmployee()
        {
            return _dbContext.tbl_EmployeeMaster .ToList();
        }
        public Employee GetEmployeeByID(int employeeId)
        {

            return _dbContext.tbl_EmployeeMaster.Find(employeeId);


        }

        public IEnumerable<string> GetBatchNumbersByEmployeeId(int employeeId)
        {
            return _dbContext.tbl_Batch
                             .Where(b => b.SitemanagerId == employeeId)
                             .Select(b => b.BatchNo)
                             .ToList();
        }

    }
}
