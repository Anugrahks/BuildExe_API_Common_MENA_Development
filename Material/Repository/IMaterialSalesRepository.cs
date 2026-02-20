using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialSalesRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialSales> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialSales> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<string> GetWorkOrder(int CustomerId, DateTime SalesDate, int MaterialTypeId);
        Task<string> GetForWareHouse(int BranchId);
        Task<string> GetforReport(MaterialSearch materialSearch);
        Task<string> GetWorkOrderMaterial(int SaleOrderId, int Id);
        Task<string> GetOrdersForSaleOrder(int CustomerId, int MaterialId, int Id);
        Task<string> GetPresentStock(int StockPoint, int CustomerId, DateTime Date);
    }
}
