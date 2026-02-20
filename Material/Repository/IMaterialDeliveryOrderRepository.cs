using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialDeliveryOrderRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialDeliveryOrderMaster> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialDeliveryOrderMaster> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);

        Task<string> GetChildTable(int ProjectId, int BlockId, int FloorId, int UnitId, int DivisionId, int Id);


        Task<string> getDeliveryOrderForPartbill(int ProjectId, int BlockId, int FloorId, int UnitId, int DivisionId, int Id);
        Task<string> GetForReport(MaterialSearch material);


        //   Task<string> GetDeliveryOrderReport(MaterialSearch materialSearch);
    }
}
