using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IContractorWorkOrderRepository
    {
        Task<IEnumerable<ContractorWorkOrder>> Get(int companyid, int branchid);
        Task<IEnumerable<ContractorWorkOrder>> GetbyID(int Id);
        Task<string> Getcontractorbyprojectid(int projectId, int blockid, int floorid, int unitid, int divisionid);
        Task<string> Getworkorderbycontractorid(int contractorid, int projectid);

        Task<string> GetStageDetailsbyid(int Id);
        Task<IEnumerable<Validation>> Insert(IEnumerable<ContractorWorkOrder> contractorWorkOrders);
        Task Delete(int Id, int userid);
        Task<IEnumerable<Validation>> Update(IEnumerable<ContractorWorkOrder> contractorWorkOrders);
        Task<IEnumerable<ContractorWorkOrderList>> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<ContractorWorkOrderList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<ContractorWorkOrderList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<ContractorWorkOrderDetails>> GetDetailsbyid(int IndentId);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<ContractorWorkOrderList>> Getforvew(HRSearch hRSearch);
        Task<IEnumerable<ContractorWorkOrderList>> getapproved(int companyid, int branchid, int UserId, int FinancialYearId);
        Task<string> GetadditionalDetailsbyid(int Id);
        Task<string> GetTaxtype(int projectid, int divisionid, int branchid, int id);
    }
}
