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

        


        //   Task<string> GetDeliveryOrderReport(MaterialSearch materialSearch);
    }
}
