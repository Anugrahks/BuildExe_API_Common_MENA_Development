using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Repository
{
    public class MaterialListRepository : IMaterialListRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {

            SelectAll = 5,
            SelectProject = 6,
            SelectProjectandSupplier = 7,
            SelectMaterial = 8,
            SelectProjectandmaterial = 9,
            Selectmaterialwithstock = 10,
            SelectmaterialwithSchedulerate = 11,
            SelectmaterialwithQuotation = 12,
            SelectMateriatRateAsPerProject = 13,
            SelectMaterialwiTHbrand = 19,
            New = 15


        }
        public MaterialListRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<MaterialList>> GetbasedonProjectandSupplier(MaterialProjectSearchList materialList, int supplierID, int finanacialyearID)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;


                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", "0");
                var FinancialYearID = new SqlParameter("@FinancialYearID", finanacialyearID);
                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", supplierID);
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectProjectandSupplier);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id,@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action,@requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialList>> GetbasedonProject(MaterialProjectSearchList materialList, int finanacialyearID)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", "0");
                var FinancialYearID = new SqlParameter("@FinancialYearID", finanacialyearID);

                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectProject);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id,@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action,@requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialList>> GetbasedonProjectAndMaterial(MaterialProjectSearchList materialList, int finanacialyearID)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var id = new SqlParameter("@id", materialList.Id);
                var Materialtypeid = new SqlParameter("@Materialtypeid", "0");
                var FinancialYearID = new SqlParameter("@FinancialYearID", finanacialyearID);

                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectProjectandmaterial);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id,@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action,@requiredDate",
                CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialList>> GetMaterialWithStock(MaterialProjectSearchList materialList)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;
                if (String.IsNullOrEmpty(materialList.MaterialTypeId.ToString()))
                    materialList.MaterialTypeId = 0;
                if (String.IsNullOrEmpty(materialList.withStock.ToString()))
                    materialList.withStock = 0;

                string[] date = materialList.RequiredDate.ToString().Split(" ");

                var CompanyId = new SqlParameter("@CompanyId", materialList.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialList.BranchId);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialList.MaterialTypeId);
                var FinancialYearID = new SqlParameter("@FinancialYearID", materialList.FinancialYearId);

                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", materialList.DivisionId);
                var withStock = new SqlParameter("@withStock ", materialList.withStock);
                var Action = new SqlParameter("@Action", Actions.Selectmaterialwithstock);
                var requiredDate = new SqlParameter("@requiredDate", date[0]);
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id," +
                    "@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action, @requiredDate",
                CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock,
                Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PurchaseReturnAll>> GetMaterialWithStock2(MaterialProjectSearchList materialList, DateTime requiredDate)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.DivisionId.ToString()))
                    materialList.DivisionId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;
                if (String.IsNullOrEmpty(materialList.withStock.ToString()))
                    materialList.withStock = 0;
                //
                string[] data = requiredDate.ToString().Split(" ");

                //
                var CompanyId = new SqlParameter("@CompanyId", materialList.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialList.BranchId);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialList.MaterialTypeId);
                var FinancialYearID = new SqlParameter("@FinancialYearID", materialList.FinancialYearId);
                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", materialList.DivisionId);
                var withStock = new SqlParameter("@withStock ", materialList.withStock);

                var Action = new SqlParameter("@Action", "15");
                var requiredDat = new SqlParameter("@requiredDate", data[0]);
                var _product = await _dbContext.tbl_PurchaseReturnAll.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId, @id, @Materialtypeid, @FinancialYearID, @ProjectId, @unitid, @blockid,@floorid, @SupplierID, @withStock, @Action, @requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDat).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int companyId, int branchid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_MaterialMaster

                                  //MaterialBrandNameMaster, materialTypeNameMaster,materialCategoryNameMaster,unitShortNameMaster
                                  select new
                                  {
                                      id = a.Id,
                                      materialID = a.MaterialID,
                                      materialName = a.MaterialName,
                                      materialTypeId = a.MaterialTypeId,
                                      materialTypeName = a.MaterialTypeNameMaster,
                                      materialBrandId = a.MaterialBrandId,
                                      // brand = b.MaterialBrandName,
                                      brand = a.MaterialBrandNameMaster,
                                      materialBrandName  = a.MaterialBrandNameMaster,
                                      materialCategoryId = a.MaterialCategoryId,
                                      // materialCategoryName = c.MaterialCategoryName,
                                      materialCategoryName = a.MaterialCategoryNameMaster,
                                      unitId = a.UnitId,

                                      unitShortName =  a.UnitShortNameMaster,

                                      materialUnitRate = a.MaterialUnitRate,
                                      taxPer = a.TaxPer,
                                      kfcPer = a.KFCPer,
                                      hsnCode = a.HsnCode,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      openigStock = a.OpenigStock,
                                      landingCost = a.LandingCost,
                                      remarks = a.Remarks,
                                      coefficientFactor = a.CoefficientFactor,
                                      coefficientUnitId = a.CoefficientUnitId,
                                      coefficientUnitName = a.CoefficientUnitName
                                  }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetByUser(int companyId, int branchid, int userid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_MaterialMaster
                                  join b in _dbContext.tbl_MaterialBrand on a.MaterialBrandId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_MaterialCategory on a.MaterialCategoryId equals c.Id into cs
                                  from c in cs.DefaultIfEmpty()
                                  join d in _dbContext.tbl_Units on a.UnitId equals d.UnitId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join f in _dbContext.tbl_MaterialType on a.MaterialTypeId equals f.MaterialTypeId
                                  select new
                                  {
                                      id = a.Id,
                                      materialID = a.MaterialID,
                                      materialName = a.MaterialName,
                                      materialTypeId = a.MaterialTypeId,
                                      materialTypeName = f.MaterialTypeName,
                                      materialBrandId = a.MaterialBrandId,
                                      // brand = b.MaterialBrandName,
                                      brand = b == null ? String.Empty : b.MaterialBrandName,
                                      materialCategoryId = a.MaterialCategoryId,
                                      // materialCategoryName = c.MaterialCategoryName,
                                      materialCategoryName = c == null ? String.Empty : c.MaterialCategoryName,
                                      unitId = a.UnitId,

                                      unitShortName = d == null ? String.Empty : d.UnitShortName,

                                      materialUnitRate = a.MaterialUnitRate,
                                      taxPer = a.TaxPer,
                                      kfcPer = a.KFCPer,
                                      hsnCode = a.HsnCode,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      openigStock = a.OpenigStock,
                                      landingCost = a.LandingCost,
                                      remarks = a.Remarks,
                                      userId = a.UserId,
                                      coefficientFactor = a.CoefficientFactor,
                                      coefficientUnitId = a.CoefficientUnitId,
                                      coefficientUnitName = a.CoefficientUnitName
                                  }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).Where(x => x.userId == userid).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialList>> Get(int companyId, int branchid, int materialtypeid)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialtypeid);
                var FinancialYearID = new SqlParameter("@FinancialYearID", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var unitid = new SqlParameter("@unitid", "0");
                var blockid = new SqlParameter("@blockid", "0");
                var floorid = new SqlParameter("@floorid", "0");
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id," +
                    "@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action, @requiredDate",
                    CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID,
                    withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> GetMaterial(int materialid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialList";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = materialid });

                cmd.Parameters.Add(new SqlParameter("@materialtypeid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BlockID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FloorID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SupplierId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@withStock", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectMaterial });
                cmd.Parameters.Add(new SqlParameter("@requiredDate", SqlDbType.Date) { Value = "2022-01-01" });

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
                if (string.IsNullOrEmpty(purcasedetails))
                {
                    purcasedetails = "[]";
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> ForStockIndividual(int MaterialId, int ProjectId, int FinancialYearId, int CompanyId, int BranchId, int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_StockBasedOnProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = MaterialId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = FinancialYearId });

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@EntryId", SqlDbType.Int) { Value = Id });
                
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 1 });

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
                if (string.IsNullOrEmpty(purcasedetails))
                {
                    purcasedetails = "[]";
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialList>> Get_Schedulerate(int companyId, int branchid, int materialtypeid, int projectId, int Unitid, int Blockid, int Floorid)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialtypeid);
                var FinancialYearID = new SqlParameter("@FinancialYearID", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var unitid = new SqlParameter("@unitid", Unitid);
                var blockid = new SqlParameter("@blockid", Blockid);
                var floorid = new SqlParameter("@floorid", Floorid);
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectmaterialwithSchedulerate);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id," +
                    "@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action, @requiredDate",
                    CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID,
                    withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }


        public async Task<IEnumerable<Materials>> GetWithQuotationBrand(int companyId, int branchid, int Projectid)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", "0");
                var FinancialYearID = new SqlParameter("@FinancialYearID", "0");
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var unitid = new SqlParameter("@unitid", "0");
                var blockid = new SqlParameter("@blockid", "0");
                var floorid = new SqlParameter("@floorid", "0");
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectmaterialwithQuotation);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMaster_combo.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId," +
                    "@id,@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action,@requiredDate",
                    CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID,
                    withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<MaterialList>> GetbasedonProjectAndMaterial(MaterialProjectSearchList materialList, DateTime requiredDate)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;
                //
                string[] data = requiredDate.ToString().Split(" ");

                //
                var CompanyId = new SqlParameter("@CompanyId", materialList.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialList.BranchId);
                var id = new SqlParameter("@id", materialList.Id);
                var Materialtypeid = new SqlParameter("@Materialtypeid", "0");
                var FinancialYearID = new SqlParameter("@FinancialYearID", materialList.FinancialYearId);

                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", materialList.DivisionId);
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectProjectandmaterial);
                var requiredDat = new SqlParameter("@requiredDate", data[0]);
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id,@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action,@requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDat).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialList>> GetByProjectId(int companyId, int branchid, int materialtypeid, int projectId, int unitId, int block, int floor)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialtypeid);
                var FinancialYearID = new SqlParameter("@FinancialYearID", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var unitid = new SqlParameter("@unitid", unitId);
                var blockid = new SqlParameter("@blockid", block);
                var floorid = new SqlParameter("@floorid", floor);
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectMateriatRateAsPerProject);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id," +
                    "@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action, @requiredDate",
                    CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID,
                    withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        //public async Task<IEnumerable<MaterialList>> ByProjectWithStock(int companyId, int branchid, int materialtypeid, int projectId, int unitId, int block, int floor, int DivisionId, int FinancialYearId)
        //{
        //    try
        //    {

        //        var CompanyId = new SqlParameter("@CompanyId", companyId);
        //        var BranchId = new SqlParameter("@BranchId", branchid);
        //        var id = new SqlParameter("@id", "0");
        //        var Materialtypeid = new SqlParameter("@Materialtypeid", materialtypeid);
        //        var FinancialYearID = new SqlParameter("@FinancialYearID", FinancialYearId);
        //        var ProjectId = new SqlParameter("@ProjectId", projectId);
        //        var unitid = new SqlParameter("@unitid", unitId);
        //        var blockid = new SqlParameter("@blockid", block);
        //        var floorid = new SqlParameter("@floorid", floor);
        //        var SupplierID = new SqlParameter("@SupplierID", DivisionId);
        //        var withStock = new SqlParameter("@withStock ", "0");
        //        var Action = new SqlParameter("@Action", "16");
        //        var requiredDate = new SqlParameter("@requiredDate", "1990-01-01");
        //        var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id," +
        //            "@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action, @requiredDate",
        //            CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID,
        //            withStock, Action, requiredDate).ToListAsync();
        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }

        //}

        public async Task<string> ByProjectWithStock(int companyId, int branchid, int materialtypeid, int projectId, int unitId, int block, int floor, int DivisionId, int FinancialYearId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_MaterialListAdjustment";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@materialtypeid", SqlDbType.Int) { Value = materialtypeid });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@BlockID", SqlDbType.Int) { Value = block });
                cmd.Parameters.Add(new SqlParameter("@FloorID", SqlDbType.Int) { Value = floor });
                cmd.Parameters.Add(new SqlParameter("@SupplierId", SqlDbType.Int) { Value = DivisionId });
                cmd.Parameters.Add(new SqlParameter("@withStock", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 16 });
                cmd.Parameters.Add(new SqlParameter("@requiredDate", SqlDbType.DateTime) { Value = "1990-01-01" });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }



        public async Task<IEnumerable<MaterialList>> GetByProjectIdWithBrand(int companyId, int branchid, int materialtypeid, int MaterialBrandId, int projectId, int unitId, int block, int floor)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialtypeid);
                var FinancialYearID = new SqlParameter("@FinancialYearID", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var unitid = new SqlParameter("@unitid", unitId);
                var blockid = new SqlParameter("@blockid", block);
                var floorid = new SqlParameter("@floorid", floor);
                var SupplierID = new SqlParameter("@SupplierID", MaterialBrandId);
                var withStock = new SqlParameter("@withStock ", "0");
                var Action = new SqlParameter("@Action", Actions.SelectMaterialwiTHbrand);
                var requiredDate = new SqlParameter("@requiredDate", "");
                var _product = await _dbContext.tbl_MaterialMasterlist.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId,@id," +
                    "@Materialtypeid,@FinancialYearID,@ProjectId,@unitid,@blockid,@floorid ,@SupplierID,@withStock,@Action, @requiredDate",
                    CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID,
                    withStock, Action, requiredDate).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
                public async Task<IEnumerable<PurchaseReturnAll>> GetbasedonProjectAndMaterialAll(MaterialProjectSearchList materialList, DateTime requiredDate)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;
                if (String.IsNullOrEmpty(materialList.MaterialTypeId.ToString()))
                    materialList.MaterialTypeId = 0;
                //
                string[] data = requiredDate.ToString().Split(" ");

                //
                var CompanyId = new SqlParameter("@CompanyId", materialList.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialList.BranchId);
                var id = new SqlParameter("@id", "0");
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialList.MaterialTypeId);
                var FinancialYearID = new SqlParameter("@FinancialYearID", materialList.FinancialYearId);
                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", materialList.DivisionId);
                var withStock = new SqlParameter("@withStock ", "0");

                var Action = new SqlParameter("@Action", "14");
                var requiredDat = new SqlParameter("@requiredDate", data[0]);
                var _product = await _dbContext.tbl_PurchaseReturnAll.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId, @id, @Materialtypeid, @FinancialYearID, @ProjectId, @unitid, @blockid,@floorid, @SupplierID, @withStock, @Action, @requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDat).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseReturnAll>> GetbasedonProjectAndMaterialEdit(MaterialProjectSearchList materialList, DateTime requiredDate)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;
                if (String.IsNullOrEmpty(materialList.MaterialTypeId.ToString()))
                    materialList.MaterialTypeId = 0;
                //
                string[] data = requiredDate.ToString().Split(" ");

                //
                var CompanyId = new SqlParameter("@CompanyId", materialList.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialList.BranchId);
                var id = new SqlParameter("@id", materialList.Id);
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialList.MaterialTypeId);
                var FinancialYearID = new SqlParameter("@FinancialYearID", materialList.FinancialYearId);
                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", materialList.UnitId);
                var blockid = new SqlParameter("@blockid", materialList.BlockId);
                var floorid = new SqlParameter("@floorid", materialList.FloorId);
                var SupplierID = new SqlParameter("@SupplierID", materialList.DivisionId);
                var withStock = new SqlParameter("@withStock ", "0");

                var Action = new SqlParameter("@Action", "16");
                var requiredDat = new SqlParameter("@requiredDate", data[0]);
                var _product = await _dbContext.tbl_PurchaseReturnAll.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId, @id, @Materialtypeid, @FinancialYearID, @ProjectId, @unitid, @blockid,@floorid, @SupplierID, @withStock, @Action, @requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDat).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PurchaseReturnAll>> GetbasedonSale(MaterialProjectSearchList materialList, DateTime requiredDate)
        {
            try
            {
                if (String.IsNullOrEmpty(materialList.ProjectId.ToString()))
                    materialList.ProjectId = 0;
                if (String.IsNullOrEmpty(materialList.BlockId.ToString()))
                    materialList.BlockId = 0;
                if (String.IsNullOrEmpty(materialList.FloorId.ToString()))
                    materialList.FloorId = 0;
                if (String.IsNullOrEmpty(materialList.UnitId.ToString()))
                    materialList.UnitId = 0;
                if (String.IsNullOrEmpty(materialList.Id.ToString()))
                    materialList.Id = 0;
                //
                string[] data = requiredDate.ToString().Split(" ");

                //
                var CompanyId = new SqlParameter("@CompanyId", materialList.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialList.BranchId);
                var id = new SqlParameter("@id", materialList.Id);
                var Materialtypeid = new SqlParameter("@Materialtypeid", materialList.MaterialTypeId);
                var FinancialYearID = new SqlParameter("@FinancialYearID", materialList.FinancialYearId);
                var ProjectId = new SqlParameter("@ProjectId", materialList.ProjectId);
                var unitid = new SqlParameter("@unitid", "0");
                var blockid = new SqlParameter("@blockid", "0");
                var floorid = new SqlParameter("@floorid", "0");
                var SupplierID = new SqlParameter("@SupplierID", "0");
                var withStock = new SqlParameter("@withStock ", "0");

                var Action = new SqlParameter("@Action", "18");
                var requiredDat = new SqlParameter("@requiredDate", data[0]);
                var _product = await _dbContext.tbl_PurchaseReturnAll.FromSqlRaw("Stpro_MaterialList @CompanyId, @BranchId, @id, @Materialtypeid, @FinancialYearID, @ProjectId, @unitid, @blockid,@floorid, @SupplierID, @withStock, @Action, @requiredDate", CompanyId, BranchId, id, Materialtypeid, FinancialYearID, ProjectId, unitid, blockid, floorid, SupplierID, withStock, Action, requiredDat).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getstock(int companyId, int branchid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_MaterialMaster
                                  join b in _dbContext.tbl_MaterialBrand on a.MaterialBrandId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_MaterialCategory on a.MaterialCategoryId equals c.Id into cs
                                  from c in cs.DefaultIfEmpty()
                                  join d in _dbContext.tbl_Units on a.UnitId equals d.UnitId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join f in _dbContext.tbl_MaterialType on a.MaterialTypeId equals f.MaterialTypeId
                                  where a.OpenigStock > 0
                                  select new
                                  {
                                      id = a.Id,
                                      materialID = a.MaterialID,
                                      materialName = a.MaterialName,
                                      materialTypeId = a.MaterialTypeId,
                                      materialTypeName = f.MaterialTypeName,
                                      materialBrandId = a.MaterialBrandId,
                                      // brand = b.MaterialBrandName,
                                      brand = b == null ? String.Empty : b.MaterialBrandName,
                                      materialCategoryId = a.MaterialCategoryId,
                                      // materialCategoryName = c.MaterialCategoryName,
                                      materialCategoryName = c == null ? String.Empty : c.MaterialCategoryName,
                                      unitId = a.UnitId,

                                      unitShortName = d == null ? String.Empty : d.UnitShortName,

                                      materialUnitRate = a.MaterialUnitRate,
                                      taxPer = a.TaxPer,
                                      kfcPer = a.KFCPer,
                                      hsnCode = a.HsnCode,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      openigStock = a.OpenigStock,
                                      landingCost = a.LandingCost,
                                      remarks = a.Remarks,
                                      coefficientFactor = a.CoefficientFactor,
                                      coefficientUnitId = a.CoefficientUnitId,
                                      coefficientUnitName = a.CoefficientUnitName,
                                  }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
