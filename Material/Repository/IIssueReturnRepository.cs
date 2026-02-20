using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IIssueReturnRepository
    {
        Task<IEnumerable<Validation>> PostIssue(IEnumerable<MaterialIssue> issue);
        Task<IEnumerable<Validation>> PutIssue(IEnumerable<MaterialIssue> issue);
        Task<string> GetIssue(int CompanyId, int BranchId, int FinacialYearId, int UserId);
        Task<string> GetApprovalIssue(int CompanyId, int BranchId, int FinacialYearId, int UserId);
        Task<string> GetByIdIssue(int Id);
        Task<string> GetDeleteIssue(int Id, int UserId);
        Task<IEnumerable<Validation>> PostReturn(IEnumerable<MaterialReturn> issue);
        Task<IEnumerable<Validation>> PutReturn(IEnumerable<MaterialReturn> issue);
        Task<string> GetReturn(int CompanyId, int BranchId, int FinacialYearId, int UserId);
        Task<string> GetApprovalReturn(int CompanyId, int BranchId, int FinacialYearId, int UserId);
        Task<string> GetDeleteReturn(int Id, int UserId);
        Task<int> Getorderno(int CompanyId, int BranchId, int FinancialYearId); 
        Task<int> GetordernoReturn(int CompanyId, int BranchId, int FinancialYearId);
        Task<string> GetStockProjectDash(int ProjectId, int CompanyId, int Branchid, int DivisionId, int FinancialYearId, int UnitId);
        Task<string> GetStockMaterialDash(int MaterialId, int CompanyId, int Branchid, int FinancialYearId);
        Task<string> GetByIdReturn(int Id);
        Task<string> GetIssueType(int CompanyId, int BranchId, int TypeId, int ProjectId, int DivisionId, int MaterialTypeId, int Id, DateTime ReturnDate);
        Task<string> GetStockDash(int ProjectId, int CategoryId, int TypeId, int DivisionId, int Branchid, int Id, int FinancialYearId);
        Task<string> GetforReport(MaterialSearch materialSearch);
    }
}
