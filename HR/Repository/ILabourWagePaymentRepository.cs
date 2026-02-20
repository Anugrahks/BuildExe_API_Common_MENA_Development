using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ILabourWagePaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<LabourWagePayment> labourWagePayments);
        Task<IEnumerable<Validation>> Update(IEnumerable<LabourWagePayment> labourWagePayments);
        Task<IEnumerable<LabourWagePayment>> GetbyID(int Id);
        Task<IEnumerable<LabourWageForPayment>> PendingBillsClearList(LabourForPayment labourForPayment);
        Task<IEnumerable<LabourWageForPayment>> GetForPaymentList(LabourForPayment labourForPayment);
        Task<IEnumerable<LabourWagePayment>> Get(int companyid, int Branchid);
        Task<IEnumerable<LabourWagePaymentList>> GetforEdit(int companyid, int Branchid);
        Task<IEnumerable<LabourWagePaymentList>> GetforEdituser(int companyid, int Branchid, int UserId, int IsBulk, int FinancialYearId);
        Task<IEnumerable<LabourWagePaymentList>> Getforapproval(int companyid, int Branchid, int Userid, int IsBulk, int FinancialYearId);
        Task<string> GetDetailsbyid(int id, int LabourGroupId);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<LabourWageForPayment>> GetForPayment(int CompanyId, int BranchId, int EmployeeId, int sitemanagerId, int FinancialyearId, DateTime dateto);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<LabourWagePaymentList>> Getforview(HRSearch hRSearch);
        Task<IEnumerable<Validation>> ValidationCheck(int CompanyId, int BranchId, int EmployeeId, int FinancialyearId, DateTime dateto, int UserId, int EmployeeGroupLabourId);

        Task<IEnumerable<Validation>> ValidationChecknew(int CompanyId, int BranchId, int EmployeeId, int FinancialyearId, DateTime Dateto, int UserId, int EmployeeLabourGroupId);
        Task<IEnumerable<ListLabour>> GetLabours(int CompanyId, int BranchId, DateTime date);
        Task<IEnumerable<LabourWageForPayment>> PendingBillsClear(int CompanyId, int BranchId, int EmployeeId, int SitemanagerId, int FinancialyearId, DateTime Dateto);
    }
}
