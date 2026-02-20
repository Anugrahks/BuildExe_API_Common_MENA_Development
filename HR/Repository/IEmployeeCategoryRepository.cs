using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IEmployeeCategoryRepository
    {
       Task <IEnumerable<EmployeeCategory >> Get();
       Task<IEnumerable<EmployeeCategory>> GetAdvance();

        Task Insert(EmployeeCategory employeeCategory  );
        Task<IEnumerable<Employee>> GetbyCategoryPersonal(int Companyid, int Branchid, int EmployeeCategory);
    }
}
