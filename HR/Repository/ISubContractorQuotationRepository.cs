using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;

namespace BuildExeHR.Repository
{
    public interface ISubContractorQuotationRepository
    {
        Task<IEnumerable<Validation>> Post(IEnumerable<SubContractorQuotation> subContractorQuotation);
        Task<IEnumerable<Validation>> Put(IEnumerable<SubContractorQuotation> subContractorQuotation);
        Task<string> Delete(int Id, int UserId);
        Task<string> GetForEdit(int CompanyId, int BranchId, int FinacialYearId, int UserId);
        Task<string> GetForApproval(int CompanyId, int BranchId, int FinacialYearId, int UserId);
        Task<string> GetById(int Id);
        Task<string> GetQuotationNo(int CompanyId, int BranchId, int FinacialYearId);
        Task<string> GetQuotationId(int BranchId, int FinacialYearId);
        Task<string> GetForIndent(int ProjectId, int BranchId, int FinacialYearId);
    }
}
