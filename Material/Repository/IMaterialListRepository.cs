using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using Microsoft.VisualBasic;

namespace BuildExeMaterialServices.Repository
{
   public interface IMaterialListRepository
    {
       Task <IEnumerable<MaterialList >> Get(int CompanyId, int Branchid,int materialtypeid);
        Task<string> Get(int CompanyId, int Branchid);
        Task<string> GetByUser(int companyId, int branchid, int userId);
        Task<IEnumerable<MaterialList>> GetbasedonProject(MaterialProjectSearchList materialList , int finanacialyearID);
        Task<IEnumerable<MaterialList>> GetbasedonProjectAndMaterial(MaterialProjectSearchList materialList, int finanacialyearID);
        Task<IEnumerable<MaterialList>> GetbasedonProjectandSupplier(MaterialProjectSearchList materialList, int SupplierID,int finanacialyearID);
        Task<IEnumerable<MaterialList>> GetMaterialWithStock(MaterialProjectSearchList materialList);
        Task<IEnumerable<PurchaseReturnAll>> GetMaterialWithStock2(MaterialProjectSearchList materialList, DateTime requiredDate);
        Task<string> GetMaterial(int materialid);
        Task<IEnumerable<MaterialList>> Get_Schedulerate(int companyId, int branchid, int materialtypeid, int projectId, int Unitid, int Blockid, int Floorid);
        Task<IEnumerable<Materials>> GetWithQuotationBrand(int companyId, int branchid, int Projectid);
        Task<IEnumerable<MaterialList>> GetbasedonProjectAndMaterial(MaterialProjectSearchList materialList, DateTime requiredDate);
        Task<IEnumerable<MaterialList>> GetByProjectId(int companyId, int branchid, int materialtypeid, int projectId, int unitId, int blockid, int floorid);

        Task<string> ByProjectWithStock(int companyId, int branchid, int materialtypeid, int projectId, int unitId, int block, int floor, int DivisionId, int FinancialYearId);
        Task<IEnumerable<MaterialList>> GetByProjectIdWithBrand(int companyId, int branchid, int materialtypeid,int MaterialBrandId, int projectId, int unitId, int blockid, int floorid);

        
        Task<IEnumerable<PurchaseReturnAll>> GetbasedonProjectAndMaterialAll(MaterialProjectSearchList materialList, DateTime requiredDate);
        Task<IEnumerable<PurchaseReturnAll>> GetbasedonProjectAndMaterialEdit(MaterialProjectSearchList materialList, DateTime requiredDate);
        Task<IEnumerable<PurchaseReturnAll>> GetbasedonSale(MaterialProjectSearchList materialList, DateTime requiredDate);
        Task<string> Getstock(int CompanyId, int Branchid);
        Task<string> ForStockIndividual(int MaterialId, int ProjectId, int FinancialYearId, int CompanyId, int BranchId, int Id);
    }
}
