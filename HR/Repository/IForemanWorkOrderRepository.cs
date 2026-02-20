using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;

namespace BuildExeHR.Repository
{
    public interface IForemanWorkOrderRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ForemanWorkOrder > foremanWorkOrders );
        Task<IEnumerable<Validation>> Update(IEnumerable<ForemanWorkOrder> purchase);
        Task<IEnumerable<ForemanWorkOrder>> GetbyID(int Id);

        Task<IEnumerable<ForemanWorkOrder>> Get(int companyid,int userid);
        Task<IEnumerable<ForemanWorkOrder>> Get(int ProjectId, int UnitId,int BlockId,int FloorId,int foremanID, int DivisionId);
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task<string> GetWorkorder(int Companyid,int BranchID);
        Task<string> Getdetsils(int Id);
        Task<string> GetdetsilsforBill(int Id);

        Task<IEnumerable<ForemanWorkOrderList>> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<ForemanWorkOrderList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task< IEnumerable<ForemanWorkOrderList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<ForemanWorkOrderList>> GetforView(HRSearch hRSearch);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<ForemanWorkOrderList>> GetforApproveduser(int companyid, int branchid, int UserId, int FinancialYearId);
    }
}
