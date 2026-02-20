using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface ILeaveApplicationRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<LeaveApplication> LeaveApplication);
        Task<IEnumerable<Validation>> Update(IEnumerable<LeaveApplication> LeaveApplication);
        Task<IEnumerable<Validation>> Delete(int id, int Userid);
        Task<IEnumerable<LeaveApplication>> GetbyID(int Idworkorder);
        Task<string> GetdetailsbyID(int Id);
        Task<IEnumerable<LeaveApplicationList>> GetforApproval(int companyid, int branchid, int Userid, int FinancialYearId);
        Task<IEnumerable<LeaveApplicationList>> GetforEdit(int companyid, int branchid, int userId, int FinancialYearId);
        Task<IEnumerable<LeaveApplicationList>> GetforAccountClearence(int companyid, int branchid);
        Task<IEnumerable<LeaveApplicationList>> GetLastLeave(int Employeeid);
        Task<IEnumerable<LeaveApplicationDocument>> GetleaveDocuments(int Id);
        Task<string> LeaveValidation(DateTime fromdate, int employeeid, int leaveid, int financialYearId);
        Task<string> LeaveValidationDuration(DateTime fromdate, int employeeid, int leaveid, int financialYearId, int DurationId);

        Task<string> Status(int CompanyId, int BranchId, int Category, string status);
        Task<string> LeaveValidationmonthly(int monthid,int yearid, int employeeid, int leaveid, int financialYearId);

        Task<string> LeaveValidationmonthlyDuration(int monthid, int yearid, int employeeid, int leaveid, int financialYearId, DateTime FromDate, DateTime ToDate);


        Task<IEnumerable<Validation>> Datevalidation(DateTime fromdate, int employeeid);

        Task<string> LeaveApplication(int CompanyId, int BranchId);

        Task<IEnumerable<LeaveApplicationList>> GetforEditMobile(int companyid, int branchid, int userId, int FinancialYearId);

        Task<IEnumerable<Validation>> DatevalidationMobile(DateTime fromdate, int employeeid);


        Task<IEnumerable<Validation>> InsertMobile(IEnumerable<LeaveApplication> LeaveApplication);


        Task<string> LeaveValidationDurationMobile(DateTime fromdate, int employeeid, int leaveid, int financialYearId, int DurationId);

        Task<string> AppliedAnnualLeaves(HRSearch hRSearch);
        Task<string> DetailsForAnnLv(int Id);
        Task<string> GetEmployeeSettlements(HRSearch hRSearch);
        Task<string> GetSalaryHeads(HRSearch hRSearch);

    }
}
