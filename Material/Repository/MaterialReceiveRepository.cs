using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class MaterialReceiveRepository : IMaterialReceiveRepository
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
            Selecdetails = 7,
            Selectforview = 8
        }

        public MaterialReceiveRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialReciept> materialReciept)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialReciept));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialRecieve @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int Userid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", Userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialRecieve @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<MaterialReciept> materialReciept)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialReciept));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialRecieve @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialReciept>> Get(int CompanyId, int Branchid)
        {
            try
            {// List<MaterialReciept> nestedpurchase = new List<MaterialReciept>();
                if (Branchid == 0)
                {
                    var purchaselist = await _dbContext.tbl_MaterialReceiveMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_MaterialReceiveDetails.Where(x => x.MaterialRecieptId == purdetail.Id).ToListAsync();
                    }
                    return purchaselist;
                }
                else
                {
                    var purchaselist = await _dbContext.tbl_MaterialReceiveMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == Branchid).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_MaterialReceiveDetails.Where(x => x.MaterialRecieptId == purdetail.Id).ToListAsync();
                    }
                    return purchaselist;
                }
                //var materialId = new SqlParameter("@materialId", "0");
                //var item = new SqlParameter("@item", "");
                //var CompanyId = new SqlParameter("@CompanyId", "0");
                //var BranchId = new SqlParameter("@BranchId", "0");

                //var Action = new SqlParameter("@Action", Actions.SelectAll);

                //var _product = _dbContext.tbl_MaterialReceiveMaster.FromSqlRaw("Stpro_MaterialRecieve @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action).ToList();
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialReciept>> GetbyID(int Id)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_MaterialReceiveMaster.Where(x => x.Id == Id).Where(x => x.IsDeleted == 0).ToListAsync();
                var purchasedetaillist = await _dbContext.tbl_MaterialReceiveDetails.Where(x => x.MaterialRecieptId == Id).ToListAsync();
                return purchaselist;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //var materialId = new SqlParameter("@materialId", Id);
            //var item = new SqlParameter("@item", "");
            //var CompanyId = new SqlParameter("@CompanyId", "0");
            //var BranchId = new SqlParameter("@BranchId", "0");

            //var Action = new SqlParameter("@Action", Actions.Select);

            //var purchaseList = _dbContext.tbl_MaterialReceiveMaster.FromSqlRaw("Stpro_MaterialRecieve @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action).ToList();
            //return purchaseList;
        }
        public async Task<IEnumerable<MaterialRecieveList>> GetforApproval(int companyId, int branchid, int UserID, int FinancialYearId,int IsAsset)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", $"{{\"IsAsset\":{IsAsset}}}");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", UserID);

                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_MaterialReceiveMasterlist.FromSqlRaw("Stpro_MaterialRecieveForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialRecieveList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_MaterialReceiveMasterlist.FromSqlRaw("Stpro_MaterialRecieveForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialRecieveList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId,int IsAsset)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", $"{{\"IsAsset\":{IsAsset}}}");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userid);

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_MaterialReceiveMasterlist.FromSqlRaw("Stpro_MaterialRecieveForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<MaterialRecieveList>> GetforView(MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_MaterialReceiveMasterlist.FromSqlRaw("Stpro_MaterialRecieveForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDetailWithTransferQtyid(int MaterialRecieptId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialRecieve";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = MaterialRecieptId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Selecdetails });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDetailsbyid(int MaterialRecieptId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_MaterialReceiveDetails
                                  join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()



                                  select new
                                  {
                                      recieptDetailId = a.RecieptDetailId,
                                      materialRecieptId = a.MaterialRecieptId,
                                      materialId = a.MaterialId,
                                      //materialName = b.MaterialName,
                                      materialName = b == null ? String.Empty : b.MaterialName,
                                      unitId = b.UnitId,
                                      //unitShortName = c.UnitShortName,
                                      unitShortName = c == null ? String.Empty : c.UnitShortName,
                                      quantity = a.Quantity,
                                      rate = a.Rate,
                                      tax = a.Tax

                                  }).Where(x => x.materialRecieptId == MaterialRecieptId).ToListAsync();
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

                cmd.CommandText = "dbo.Stpro_MaterialRecieve";
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

                //var materialId = new SqlParameter("@materialId", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                //var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                //var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                //var UserId = new SqlParameter("@UserId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectReport);
                //var _product = _dbContext.tbl_MaterialReceiveMasterlist.FromSqlRaw("Stpro_MaterialRecieve @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action);
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return null;
            }
        }
    }
}
