using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISubContractorBillRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorBill > subContractorBill);
        Task<IEnumerable<Validation>> InsertRate(IEnumerable<SubContractorRateRevision> subContractorBills);
        Task<string> raterevision(int WorkOrderId, int WorkId);
        Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorBill> subContractorBill);
        Task< IEnumerable<SubContractorBill>> GetbyID(int Id);

        Task<string> purchaseadjustment(int Id, int SubcontractorId, int ProjectId, int BlockId, int FloorId, int UnitId);

        Task<IEnumerable<SubContractorBill>> Get(int Companyid,int branchid);
        Task Delete(int id,int Userid);
        Task<IEnumerable<SubContractorBillList >> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<SubContractorBillList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<SubContractorBillList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<SubContractorBillList>> Getforview(HRSearch hRSearch);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetadditionalDetailsbyid(int IndentId);
        Task<string> raterevision(int WorkOrderId);
        Task<string> subcontractorworkorder(int ProjectId, int BlockId, int FloorId, int UnitId);
        Task<string> GetPreviousBillQty(int BillId, int WorkOrderId);

        Task<string> GetSubworkList(int WorkOrderId);
        Task<IEnumerable<SubContractorBill>> GetLastBill(int workorderid);
        Task<string> Getjson(HRSearch hRSearch);
        Task<DateTime> GetBillFromDate(int workorderid);

        Task<string> GetReportforRateRevision(HRSearch hRSearch);
        Task<string> AccountheadOther(int id);


        Task<string> OtherDeductionsGet(int id);

        Task<string> GetOtherDeductionPer(int BranchId);

        Task<string> GetLabel(int BranchId);

        Task<string> GetPreviousBalance(int WorkOrderId, int SubcontractorId);
    }
}
