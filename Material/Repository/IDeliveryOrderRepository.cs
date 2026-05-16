using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;

namespace BuildExeMaterialServices.Repository
{
    public interface IDeliveryOrderRepository
    {
        Task<IEnumerable<Validation>> Update(DeliveryOrderMaster mat);   // changed
        Task<IEnumerable<Validation>> Insert(DeliveryOrderMaster mat);   // changed
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<string> GetDeliveryOrderReport(MaterialSearch materialSearch);
    }
}