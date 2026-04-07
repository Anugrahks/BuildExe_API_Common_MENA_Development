using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServiceManagement.Models;
namespace BuildExeServiceManagement.Repository
{
    public interface IPumpModuleRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<PumpModuleRequest> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<PumpModuleRequest> mat);

        Task<IEnumerable<Validation>> InsertElectricalTest(IEnumerable<ElectricalTestRequest> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId, int Type);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId, int Type);
        Task<string> GetById(int Id);

        Task<string> getElectricalTest(int Id);

        Task<string> getAutoFetch(int BranchId, int TypeId);

        Task<string> GetPumbDetails(int CompanyId, int Branchid, int UserId, int FinancialYearId, int StockPointId);

        Task<string> GetServiceLookUp(int CustomerId, int CompanyId, int BranchId);       

        Task<string> GetServiceQuotation(int CompanyId, int BranchId);       
        Task<IEnumerable<Validation>> InsertQuotation(PumpModuleRequest mat);  
        Task<IEnumerable<Validation>> UpdateQuotation(PumpModuleRequest mat);   
        Task<string> GetServiceQuotationListings(int CompanyId, int BranchId, int FinancialYearId, int UserId); 
        Task<IEnumerable<Validation>> DeleteQuotation(int Id, int UserID);      
        Task<string> GetByIdQuotation(int Id);    
        Task<string> GetClientApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);  
        Task<IEnumerable<Validation>> ClientApprovalUpdate(PumpModuleRequest mat);  
        Task<string> GetPumpAutoFetch(int CompanyId, int BranchId); 
        Task<string> GetJobAutoFetch(int CompanyId, int BranchId); 
        //   Task<string> GetDeliveryOrderReport(MaterialSearch materialSearch);
    }
}
