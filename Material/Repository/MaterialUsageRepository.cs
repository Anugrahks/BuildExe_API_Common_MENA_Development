using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class MaterialUsageRepository : IMaterialUsageRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectReport = 6,
            Selectforview = 7
        }

        public MaterialUsageRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialUsage> materialUsage)
        {

            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialUsage));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialUsage @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int userid)
        {
            try
            {
                var PId = new SqlParameter("@Id", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialUsage @Id,@item, @CompanyId, @BranchId,@UserId, @Action", PId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<MaterialUsage> materialUsage)
        {
            try
            {
                var PId = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialUsage));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialUsage @Id,@item, @CompanyId, @BranchId,@UserId, @Action", PId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialUsage>> Get(int CompanyId, int Branchid)
        {
            try
            {
                //var PId = new SqlParameter("@Id", "0");
                //var item = new SqlParameter("@item", "");
                //var CompanyId = new SqlParameter("@CompanyId", "0");
                //var BranchId = new SqlParameter("@BranchId", "0");
                //var UserId = new SqlParameter("@UserId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectAll);

                //var _product = _dbContext.tbl_MaterialUsageMaster.FromSqlRaw("Stpro_MaterialUsage @Id,@item, @CompanyId, @BranchId,@UserId, @Action", PId, item, CompanyId, BranchId, UserId, Action).ToList();
                //return _product;
                if (Branchid == 0)
                {
                    var purchaselist = await _dbContext.tbl_MaterialUsageMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_MaterialUsageDetails.Where(x => x.MaterialUsageId == purdetail.Id).ToListAsync();
                    }

                    return purchaselist;
                }
                else
                {
                    var purchaselist = await _dbContext.tbl_MaterialUsageMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == Branchid).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_MaterialUsageDetails.Where(x => x.MaterialUsageId == purdetail.Id).ToListAsync();
                    }

                    return purchaselist;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialUsage>> GetbyID(int Id)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_MaterialUsageMaster.Where(x => x.Id == Id).Where(x => x.IsDeleted == 0).ToListAsync();
                var purchasedetaillist = await _dbContext.tbl_MaterialUsageDetails.Where(x => x.MaterialUsageId == Id).ToListAsync();
                return purchaselist;
                //var PId = new SqlParameter("@Id", Id);
                //var item = new SqlParameter("@item", "");
                //var CompanyId = new SqlParameter("@CompanyId", "0");
                //var BranchId = new SqlParameter("@BranchId", "0");
                //var UserId = new SqlParameter("@UserId", "0");
                //var Action = new SqlParameter("@Action", Actions.Select);

                //var _product = _dbContext.tbl_MaterialUsageMaster.FromSqlRaw("Stpro_MaterialUsage @Id,@item, @CompanyId, @BranchId,@UserId, @Action", PId, item, CompanyId, BranchId, UserId, Action).ToList();
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialusageList>> GetforApproval(int companyId, int branchid, int UserID,int FinancialYearId, int bulkentry)
        {
            try
            {
                var Id = new SqlParameter("@Id", bulkentry);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", UserID);

                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_MaterialUsagelist.FromSqlRaw("Stpro_MaterialIssueForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialusageList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_MaterialUsagelist.FromSqlRaw("Stpro_MaterialIssueForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialusageList>> GetforEdit(int companyId, int branchid, int userid,int FinancialYearId, int bulkentry)
        {
            try
            {
                var Id = new SqlParameter("@Id", bulkentry);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userid);

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_MaterialUsagelist.FromSqlRaw("Stpro_MaterialIssueForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialusageList>> Getforview(MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_MaterialUsagelist.FromSqlRaw("Stpro_MaterialIssueForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDetailsbyid(int MaterialUsageId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_MaterialUsageDetails
                                  join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId
                                  select new
                                  {
                                      materialUsageDetailsId = a.MaterialUsageDetailsId,
                                      materialUsageId = a.MaterialUsageId,
                                      materialId = a.MaterialId,
                                      materialName = b.MaterialName,
                                      unitId = b.UnitId,
                                      materialTypeId = b.MaterialTypeId,
                                      unitShortName = c.UnitShortName,
                                      quantity = a.Quantity,
                                      rate = a.Rate,
                                      coefficientFactorValue = a.CoefficientFactorValue,
                                      conversionQuantity = a.ConversionQuantity,
                                      conversionUnitName = a.ConversionUnitName


                                  }).Where(x => x.materialUsageId == MaterialUsageId).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetforReport(MaterialSearch materialSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialUsage";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReport });
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

                return null;
            }
        }

        public async Task<IEnumerable<Validation>> Getvalidation(int ProjectId, DateTime UsageDate,int FinancialYearId)
        {

            try
            {
                var Projectid = new SqlParameter("@ProjectId", ProjectId);
                var Usagedate = new SqlParameter("@UsageDate", UsageDate);
                var FinancialYearid = new SqlParameter("@FinancialYearId", FinancialYearId);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_PendingApproval @ProjectId,@UsageDate, @FinancialYearId",Projectid ,Usagedate,FinancialYearid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
