using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
   public  interface IEmployeeMasterRepository
    {
        IEnumerable<Employee> GetEmployee();
        Employee GetEmployeeByID(int branchId);
       
        void InsertEmployee(Employee Employee);
        void DeleteEmployee(int EmployeeId);
        void UpdateEmployee(Employee Employee);
        void Save();

        IEnumerable<string> GetBatchNumbersByEmployeeId(int employeeId);
    }
}
