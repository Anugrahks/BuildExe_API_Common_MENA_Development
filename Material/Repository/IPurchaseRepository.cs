using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
   public  interface IPurchaseRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<Purchase > purchase);
        Task<IEnumerable<Validation>> Update(IEnumerable<Purchase> purchase);
        Task<IEnumerable<Purchase>> GetbyID(int Id);


        Task<string> GetInvoiceNo(int companyId, int branchid);
        // string  GetbyID(int Id);
        Task<IEnumerable<Purchase>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id,int UserID);
        Task<IEnumerable<PurchaseList>> Getforapproval(int companyId, int branchid, int UserID,int menuid, int FinancialYearId, int IsAsset);
        Task<IEnumerable<PurchaseList>> GetforEdit(int companyId, int branchid, int menuid);
        Task<IEnumerable<PurchaseList>> GetforEdit(int companyId, int branchid, int userId, int menuid, int FinancialYearId, int IsAsset);
        Task<IEnumerable<PurchaseList>> Getforview(MaterialSearch materialSearch);
        Task<string> Getforapproval(int CompanyId, int Branchid);

        Task<string> GetDetailsbyid(int PurchaseMasterid);
        Task<string> GetforReport(MaterialSearch materialSearch);
        Task<string> Getjson(MaterialSearch materialSearch);
        Task<IEnumerable<MaterialSchedule>> MaterialSchedule(MaterialSearch materialSearch);
        Task<string> MaterialsSchedule(MaterialSearch materialSearch);
        Task<string> GetReport(AgeingSearch ageingSearch);

    }
}
