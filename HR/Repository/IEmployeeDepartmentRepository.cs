using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IEmployeeDepartmentRepository
    {
        Task<IEnumerable<EmployeeDepartment  >> Getdepartment(int companyid,int Branchid);
        Task<EmployeeDepartment> GetdepartmentByID(int departmentId);
        Task Insertdepartment(EmployeeDepartment department);
        Task Deletedepartment(int departmentId,int userid);
        Task Updatedepartment(EmployeeDepartment department);
        void Save();
    }
}
