using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IEmployeeMasterRepository
    {
        Task<IEnumerable<EmployeeMaster>> Get(int companyid, int Branchid);
        Task<IEnumerable<EmployeeMaster>> Getuser(int companyid, int Branchid, int Userid);
        Task<string> Getuser(int companyid, int branchid, int Userid, int Financialyearid);
        Task<string> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(EmployeeMaster employeeMaster);
        Task Delete(int Id, int userid);
        Task<IEnumerable<Validation>> Update(EmployeeMaster employeeMaster);
        //   void Save();

        Task<IEnumerable<Employee>> GetForAttendance(int ProjectId, int UnitId, int BlockId, int FloorId, int EmployeeCategory);
        Task<IEnumerable<Employee>> GetbyCategory(int CompanyId, int Branchid, int CategoryId);
        Task<IEnumerable<Employee>> GetbyCategoryandLabourgroup(int CompanyId, int Branchid, int CategoryId, int labourgroup);
        Task<IEnumerable<Validation>> getvalidation(EmployeeMaster employeeMaster);
        Task<IEnumerable<Validation>> Deletevalidation(int id);
        Task<IEnumerable<EmployeeMaster>> GetByStatus(int companyid, int branchid, string status);
        Task<IEnumerable<EmployeeMaster>> GetByStatusandCategory(int companyid, int branchid, int category, string status);
        Task<string> StatusRejoining(int companyid, int branchid);

        
        int GenerateNextEmpNo(int companyid);
        int ValidationForPunching(int EmployeeId);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
        Task<EmployeeDetail> GetEmployeeById(int EmployeeId);

        Task<string> EmployeeWithProject(int projectId, int categoryId);

        Task<string> EmployeeforProject(int employeeId, int categoryId);
        Task<string> PostValidation(EmployeeMaster employeeMaster);

        Task<string> BirthDayReminder(int BranchId, int UserId, DateTime Date);

        Task<IEnumerable<Employee>> GetForSalaryPymt(int Companyid, int Branchid, int EmployeeCategory);
        Task<IEnumerable<Employee>> GetForCheckIn(int Companyid, int Branchid, int EmployeeCategory);
    }
}
