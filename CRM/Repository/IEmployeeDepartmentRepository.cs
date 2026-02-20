using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IEmployeeDepartmentRepository
    {
        IEnumerable<EmployeeDepartment > GetEmployeeDepartment();
        EmployeeDepartment GetEmployeeDepartmentByID(int employeeDepartmentid);
        void InsertEmployeeDepartment(EmployeeDepartment employeeDepartment);
        void Delete_EmployeeDepartment(int employeeDepartmentid);
        void Update_EmployeeDepartment(EmployeeDepartment employeeDepartment);
        void Save();
    }
}
