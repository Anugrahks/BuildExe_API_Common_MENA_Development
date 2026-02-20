using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IEmployeeJoiningRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<EmployeeJoining> EmployeeJoining);
        Task<IEnumerable<Validation>> Update(IEnumerable<EmployeeJoining> EmployeeJoining);
        Task<IEnumerable<Validation>> Delete(int id, int Userid);
        Task<IEnumerable<EmployeeJoining>> GetbyID(int Idworkorder);

        Task<IEnumerable<EmployeeJoiningModel>> GetforEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<IEnumerable<EmployeeJoiningModel>> GetforApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);


        Task<IEnumerable<Validation>> InsertIssueReturn(IEnumerable<EmployeeIssueReturnMaster> EmployeeJoining);
        Task<IEnumerable<Validation>> UpdateIssueReturn(IEnumerable<EmployeeIssueReturnMaster> EmployeeJoining);
        Task<IEnumerable<Validation>> DeleteIssueReturn(int id, int Userid);


        Task<string> IssueReturnReport(HRSearch hRSearch);
        Task<string> ReturnCode(int BranchId, int Id, int EmployeeId);

        Task<string> GetbyIDIssueReturn(int Id);

        Task<string> GetforEditIssueReturn(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<string> GetforApprovalIssueReturn(int CompanyId, int BranchId, int UserId, int FinancialYearId);



        Task<string> EmployeeJoiningDocumentAndAlert(int Id);

        Task<string> EmployeeJoiningBankDetail(int Id);

        Task<string> EmployeeJoiningFacilityDetail(int Id);

        Task<string> EmployeeJoiningExpensePerDay(int Id);

        Task<string> EmployeeJoiningIssueDetail(int Id);

        Task<string> GetLastJoining(int Employeeid);


        Task<string> GetDuration(int BranchId);

        Task<string> Duration(int Id);

        Task<string> IssueCode(int BranchId, int Id, int EmployeeId);


        Task<string> IssueCodeForTransfer(int BranchId, int ProjectId, int DivisionId,int MaterialTypeId,int MaterialId);





    }
}
