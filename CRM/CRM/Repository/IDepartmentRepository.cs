using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IDepartmentRepository
    {

        Task<IEnumerable<Department >> Getdepartment();
        Task<IEnumerable<Department>> Getdepartment(int CompanyId, int BranchId);

        Task<IEnumerable<Department>> GetEmployeeDept(int CompanyId, int BranchId);

        
        Task<Department> GetdepartmentByID(int departmentId);
        Task<IEnumerable<Validation>> Insertdepartment(Department department);
        Task<IEnumerable<Validation>> Deletedepartment(int departmentId, int userid);
        Task<IEnumerable<Validation>> Updatedepartment(Department department);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int departmentId);
    }
}
