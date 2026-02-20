using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface ISalaryBillRepository
    {
        Task<string> GetSalaryBill(int companyId, int Branchid, int UserId, int MonthId, int YearId,int FinancialYearId, DateTime Date, int EmployeeId, int DurationId,DateTime FromDate, DateTime ToDate, int IsVariation);

        Task<string> EmployeeList(int Id, int companyId, int Branchid, int UserId, int MonthId, int YearId, int FinancialYearId, DateTime Date);

        
        Task<string> SalaryBillGenerator(HRSearch hRSearch);
        
        Task<IEnumerable<Validation>> GetSalaryValidation(int companyId, int branchId, int userId, int monthId, int yearId, int EmployeeId,int DurationId, DateTime fromDate, DateTime toDate);

        Task<IEnumerable<Validation>> Validation(HRSearch hRSearch);

        
        Task<string> GetByUser(int companyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetForApproval(int companyId, int branchId, int userId, int FinancialYearId);
        Task<IEnumerable<Validation>> Insert(IEnumerable<SalaryBill> salaryBillGenerationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<SalaryBill> salaryBillGenerationMaster);
        Task Delete(int id, int userid);
        Task<string> Getdetailsforview(int employeeid, int monthid, int yearId, int companyid, int branchid, int financialYearId);
        Task<string> Getdetailsforviewduration(int employeeid, int monthid, int yearId, int companyid, int branchid, int financialYearId, DateTime FromDate, DateTime ToDate);
        Task<string> Getjson(HRSearch hRSearch);

        Task<string> WPS(HRSearch hRSearch);

        Task<string> WPSSearch(HRSearch hRSearch);


        
        Task<string> SalaryBillReprint(int Id);

        Task<string> GetEmployeeList(int CompanyId, int Branchid, int CategoryId);
    }
}
