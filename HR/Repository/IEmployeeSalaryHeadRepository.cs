using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public  interface IEmployeeSalaryHeadRepository
    {
        Task<string>  Get(int Companyid,int Branchid);
        //EmployeeSalaryHead GetByID(int Id);
        Task<string> GetByID(int emplyeeId);
        Task Insert(IEnumerable<EmployeeSalaryHead> employeeSalaryHead );
        Task Delete(int emplyeeId,int userID);
        Task Update(IEnumerable<EmployeeSalaryHead> employeeSalaryHead);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int isDelete);
        Task<IEnumerable<Validation>> ChecksalaryheadDelete(int id, int employeeid, int isDelete);
    }
}
