using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
   public interface IQuotationRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<Quotation> Quotation);
        Task<IEnumerable<Validation>> Update(IEnumerable<Quotation> Quotation);
        Task<IEnumerable<Validation>> Delete(int id, int UserID);
        Task<IEnumerable<Quotation>> GetbyID(int id);

        Task<IEnumerable<Validation>> Insert_QuotationRate(IEnumerable<QuotationRate> quotationRates );
        Task<IEnumerable<Validation>> Update_QuotationRate(IEnumerable<QuotationRate> quotationRates);
        Task<IEnumerable<Validation>> Delete_QuotationRate(int id, int UserID);
        Task<string> GetDetailsbyid(int Quotationid);
        Task<string> GetRateDetailsbyid(int Quotationid);
        Task<string> GetSupplierDetails(int Quotationid);
         Task<IEnumerable<QuotationList>> GetforEdit(int companyId, int branchid);
        Task<IEnumerable<QuotationList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId);
        Task<IEnumerable<QuotationList>> Getforapproval(int companyId, int branchid, int userid, int FinancialYearId);
        Task<IEnumerable<QuotationList>> GetBy_Project(int ProjectID);
        Task<IEnumerable<QuotationList>> Getfor_Edit_Rate(int companyId, int branchid);
        Task<IEnumerable<QuotationList>> Getfor_Edit_Rate(int companyId, int branchid, int userid, int FinancialYearId);
        Task<IEnumerable<QuotationList>> Getfor_CompasisonStatement(int companyId, int branchid);
        Task<IEnumerable<QuotationList>> Getfor_CompasisonStatement(int companyId, int branchid, int userid, int FinancialYearId);
        Task<int> GetMaxquotationo(int CompanyId, int Branchid);
        Task<int> GetMaxquotationo(int CompanyId, int Branchid, int FinancialYearId);
        Task<string> Getsuppliers(int PRojectid, int MaterialId, int BrandID);
        Task<IEnumerable<Validation>> Approve_QuotationRate(IEnumerable<Quotation> Quotation);
        Task<string> GetsupplierswithItem(int QuotationID);
        Task<string> GetSupplierTotal(int QuotationID);
        Task<string> GetItemwithSupplier(int QuotationID);
        Task<string> GetApprovedData(int CompanyId, int BranchId, int ProjectID, int MaterialId);
        Task<string> GetQuotationApprovedDetails(int projectId, int materialId, int brandId, int companyId, int branchId);
        Task<IEnumerable<QuotationList>> GetforApprovalCompasisonStatement(int companyId, int branchid, int userid, int FinancialYearId);
    }
}
