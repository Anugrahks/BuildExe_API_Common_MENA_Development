using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


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
        public async Task<string> PostGetName(MaterialSearch materialSearches)
        {
            try
            {
                //    var materialId = new SqlParameter("@materialId", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearches));
                //var CompanyId = new SqlParameter("@CompanyId", materialSearches.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", materialSearches.BranchId );
                //var UserId = new SqlParameter("@UserId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectReport );
                //var _product = await _dbContext.tbl_MaterialMasterReport .FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                //return _product;
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_UniqueIdsProcedure";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearches.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearches.BranchId });
                cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar)
                {
                    Value = materialSearches.MaterialFor
                });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearches) });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = materialSearches.ActionButton });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                if (purcasedetails == "")
                    purcasedetails = "[]";
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
