using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<PurchaseOrder > purchaseOrder);
        Task<IEnumerable<Validation>> Update(IEnumerable<PurchaseOrder> purchaseOrder);
        Task<IEnumerable<PurchaseOrder>> GetbyID(int Id);
        Task<IEnumerable<PurchaseOrder>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<PurchaseOrder>> GetCombo(int ProjectId, int UnitId, int BlockId, int FloorId, int SupplierId);
        Task<IEnumerable<Validation>> Delete(int id,int Userid);
         Task<IEnumerable<PurchaseOrderList >> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<PurchaseOrderList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId, int IsAsset);
        Task<int> GetMaxOrderId(int CompanyId, int Branchid);
        Task<int> GetMaxOrderId(int CompanyId, int Branchid, int financialyear);

        Task<int> GetMaxOrderIdDelivery(int CompanyId, int Branchid, int financialyear);

        
        Task<IEnumerable<PurchaseOrderList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId, int IsAsset);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetReport(MaterialSearch materialSearch);
        Task<IEnumerable<PurchaseOrderList>> Getforview(MaterialSearch materialSearch);
        Task<string> GetpendingpurchaseOrder(int ProjectId, int UnitId, int BlockId, int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate);
        Task<string> GetpendingpurchaseOrder(int ProjectId, int UnitId, int BlockId, int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId);

        Task<string> DeliveryOrder(int ProjectId, int UnitId, int BlockId, int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId, int Id);

        Task<string> DeliveryOrderForPurchase(int ProjectId, int UnitId, int BlockId, int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId, int Id);

        

        Task<string> GetpendingpurchaseOrderdetails(int Id);

        Task<string> GetPONo(int CompanyId, int Branchid);

    }
}
