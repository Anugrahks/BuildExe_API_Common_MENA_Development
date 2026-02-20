using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.InteropServices;


namespace BuildExeMaterialServices.Repository
{
    public class MaterialListByCategoryRepository : IMaterialListByCategoryRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            TypeNCategory = 1,
            TypeCategoryNBrand = 2
        }

        public MaterialListByCategoryRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MaterialListByCategory>> Get(int companyId, int branchid, int materialtypeid, int materialcategoryid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var MaterialTypeid = new SqlParameter("@MaterialTypeid", materialtypeid);
                var MaterialCategoryId = new SqlParameter("@MaterialCategoryId", materialcategoryid);
                var MaterialBrandId = new SqlParameter("@MaterialBrandId", "0");
                var Action = new SqlParameter("@Action", Actions.TypeNCategory);

                var _product = await _dbContext.tbl_MaterialMasterlistByCategory.FromSqlRaw("[Stpro_MaterialListByCategory] @CompanyId, @BranchId, @MaterialTypeId, @MaterialCategoryId, @MaterialBrandId, @Action",
                    CompanyId, BranchId, MaterialTypeid, MaterialCategoryId, MaterialBrandId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialListByCategory>> Get(int companyId, int branchid, int materialtypeid, int materialcategoryid, int materialbrandid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var MaterialTypeid = new SqlParameter("@MaterialTypeid", materialtypeid);
                var MaterialCategoryId = new SqlParameter("@MaterialCategoryId", materialcategoryid);
                var MaterialBrandId = new SqlParameter("@MaterialBrandId", materialbrandid);
                var Action = new SqlParameter("@Action", Actions.TypeCategoryNBrand);

                var _product = await _dbContext.tbl_MaterialMasterlistByCategory.FromSqlRaw("[Stpro_MaterialListByCategory] @CompanyId, @BranchId, @MaterialTypeId, @MaterialCategoryId, @MaterialBrandId, @Action",
                    CompanyId, BranchId, MaterialTypeid, MaterialCategoryId, MaterialBrandId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
