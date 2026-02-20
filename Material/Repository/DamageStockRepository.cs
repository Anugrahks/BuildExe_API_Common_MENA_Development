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
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class DamageStockRepository:IDamageStockRepository 
    {
        private readonly MaterialContext _dbContext;

        public DamageStockRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            SelectforEdit = 6,
            Selectforapproval = 7,
            Selectforview = 8

        }
        public async Task<IEnumerable<Validation>> Insert(DamageStock damageStock)
        {
            try
            {
                if (damageStock.ApprovalRemarks == null)
                    damageStock.ApprovalRemarks = "";

                if (damageStock.RejectRemarks == null)
                    damageStock.RejectRemarks = "";


                var Id = new SqlParameter("@Id", "1");
                var EntryDate = new SqlParameter("@EntryDate", damageStock.Entrydate);
                var ProjectId = new SqlParameter("@ProjectId", damageStock.ProjectId);
                var UnitId = new SqlParameter("@UnitId", damageStock.UnitId);

                var BlockId = new SqlParameter("@BlockId", damageStock.BlockId);
                var FloorId = new SqlParameter("@FloorId", damageStock.FloorId);
                var DivisionId = new SqlParameter("@DivisionId", damageStock.DivisionId);
                var MaterialId = new SqlParameter("@MaterialId", damageStock.MaterialId);
                var Stock = new SqlParameter("@Stock", damageStock.Stock);
                var Rate = new SqlParameter("@Rate", damageStock.Rate);
                var FinantialYearId = new SqlParameter("@FinantialYearId", damageStock.FinancialYearId);

                var CompanyId = new SqlParameter("@CompanyId", damageStock.CompanyId);
                var BranchId = new SqlParameter("@BranchId", damageStock.BranchId);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", damageStock.ApprovalStatus);
                
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", damageStock.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", damageStock.ApprovedBy);

                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", damageStock.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", damageStock.RejectRemarks);
                var IsReject = new SqlParameter("@IsReject", damageStock.IsReject);
                var userid = new SqlParameter("@UserId", damageStock.UserId);

                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_DamageStock @Id, @EntryDate, @ProjectId, @UnitId, @BlockId, @FloorId,@DivisionId, @MaterialId, @Stock, @Rate, @FinantialYearId, @CompanyId, @BranchId, @ApprovalStatus, @ApprovalLevel,@ApprovedBy,@ApprovalRemarks,@RejectRemarks,@IsReject,@UserId, @Action", Id, EntryDate, ProjectId, UnitId, BlockId, FloorId, DivisionId, MaterialId, Stock, Rate, FinantialYearId, CompanyId, BranchId, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovalRemarks, RejectRemarks, IsReject, userid, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(DamageStock damageStock)
        {
            try
            {
                if (damageStock.ApprovalRemarks == null)
                    damageStock.ApprovalRemarks = "";

                if (damageStock.RejectRemarks == null)
                    damageStock.RejectRemarks = "";

                var Id = new SqlParameter("@Id", damageStock.Id);
            var EntryDate = new SqlParameter("@EntryDate", damageStock.Entrydate);
            var ProjectId = new SqlParameter("@ProjectId", damageStock.ProjectId);
            var UnitId = new SqlParameter("@UnitId", damageStock.UnitId);

            var BlockId = new SqlParameter("@BlockId", damageStock.BlockId);
            var FloorId = new SqlParameter("@FloorId", damageStock.FloorId);
                var DivisionId = new SqlParameter("@DivisionId", damageStock.DivisionId);
                var MaterialId = new SqlParameter("@MaterialId", damageStock.MaterialId);
            var Stock = new SqlParameter("@Stock", damageStock.Stock);
            var Rate = new SqlParameter("@Rate", damageStock.Rate);
            var FinantialYearId = new SqlParameter("@FinantialYearId", damageStock.FinancialYearId);

            var CompanyId = new SqlParameter("@CompanyId", damageStock.CompanyId);
            var BranchId = new SqlParameter("@BranchId", damageStock.BranchId);
            var ApprovalStatus = new SqlParameter("@ApprovalStatus", damageStock.ApprovalStatus);
            
            var ApprovalLevel = new SqlParameter("@ApprovalLevel", damageStock.ApprovalLevel);
            var ApprovedBy = new SqlParameter("@ApprovedBy", damageStock.ApprovedBy);
            var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", damageStock.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", damageStock.RejectRemarks);
                var IsReject = new SqlParameter("@IsReject", damageStock.IsReject);
            var userid = new SqlParameter("@UserId", damageStock.UserId);
            var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_DamageStock @Id, @EntryDate, @ProjectId, @UnitId, @BlockId, @FloorId,@DivisionId, @MaterialId, @Stock, @Rate, @FinantialYearId, @CompanyId, @BranchId, @ApprovalStatus, @ApprovalLevel,@ApprovedBy,@ApprovalRemarks, @RejectRemarks,@IsReject, @UserId, @Action", Id, EntryDate, ProjectId, UnitId, BlockId, FloorId, DivisionId, MaterialId, Stock, Rate, FinantialYearId, CompanyId, BranchId, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovalRemarks, RejectRemarks, IsReject, userid, Action).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<DamageStock>> Get(int Companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");

            var EntryDate = new SqlParameter("@EntryDate", "");
            var ProjectId = new SqlParameter("@ProjectId", "0");
            var UnitId = new SqlParameter("@UnitId", "0");

            var BlockId = new SqlParameter("@BlockId", "0");
            var FloorId = new SqlParameter("@FloorId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var MaterialId = new SqlParameter("@MaterialId", "0");
            var Stock = new SqlParameter("@Stock", "0");
            var Rate = new SqlParameter("@Rate", "0");
            var FinantialYearId = new SqlParameter("@FinantialYearId", "0");

            var CompanyId = new SqlParameter("@CompanyId", Companyid);
            var BranchId = new SqlParameter("@BranchId", Branchid);
            var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
            
            var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
            var ApprovedBy = new SqlParameter("@ApprovedBy", "0");

            var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
            var IsReject = new SqlParameter("@IsReject", "0");
            var userid = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectAll );

            var _product = await _dbContext.tbl_DamageStock.FromSqlRaw("Stpro_DamageStock @Id, @EntryDate, @ProjectId, @UnitId, @BlockId, @FloorId,@DivisionId, @MaterialId, @Stock, @Rate, @FinantialYearId, @CompanyId, @BranchId, @ApprovalStatus, @ApprovalLevel,@ApprovedBy,@ApprovalRemarks,@RejectRemarks,@IsReject, @UserId, @Action", Id, EntryDate, ProjectId, UnitId, BlockId, FloorId, DivisionId, MaterialId, Stock, Rate, FinantialYearId, CompanyId, BranchId, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovalRemarks, RejectRemarks, IsReject, userid, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<DamageStock>> GetByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);

            var EntryDate = new SqlParameter("@EntryDate", "");
            var ProjectId = new SqlParameter("@ProjectId", "0");
            var UnitId = new SqlParameter("@UnitId", "0");

            var BlockId = new SqlParameter("@BlockId", "0");
            var FloorId = new SqlParameter("@FloorId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var MaterialId = new SqlParameter("@MaterialId", "0");
            var Stock = new SqlParameter("@Stock", "0");
            var Rate = new SqlParameter("@Rate", "0");
            var FinantialYearId = new SqlParameter("@FinantialYearId", "0");

            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
            var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
            var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
            var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
            var userid = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Select );

            var _product = await _dbContext.tbl_DamageStock.FromSqlRaw("Stpro_DamageStock @Id, @EntryDate, @ProjectId, @UnitId, @BlockId, @FloorId,@DivisionId, @MaterialId, @Stock, @Rate, @FinantialYearId, @CompanyId, @BranchId, @ApprovalStatus, @ApprovalLevel,@ApprovedBy,@ApprovalRemarks,@RejectRemarks,@IsReject,@UserId, @Action", Id, EntryDate, ProjectId, UnitId, BlockId, FloorId, DivisionId, MaterialId, Stock, Rate, FinantialYearId, CompanyId, BranchId, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovalRemarks, RejectRemarks, IsReject,userid, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int id,int approvedBy)
        {
            try
            { 
            var Id = new SqlParameter("@Id", id);
        
            var EntryDate = new SqlParameter("@EntryDate", "");
            var ProjectId = new SqlParameter("@ProjectId", "0");
            var UnitId = new SqlParameter("@UnitId", "0");

            var BlockId = new SqlParameter("@BlockId", "0");
            var FloorId = new SqlParameter("@FloorId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var MaterialId = new SqlParameter("@MaterialId", "0");
            var Stock = new SqlParameter("@Stock", "0");
            var Rate = new SqlParameter("@Rate", "0");
            var FinantialYearId = new SqlParameter("@FinantialYearId", "0");

            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
            var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
            var ApprovedBy = new SqlParameter("@ApprovedBy", approvedBy);

            var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
            var userid = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_DamageStock @Id, @EntryDate, @ProjectId, @UnitId, @BlockId, @FloorId,@DivisionId, @MaterialId, @Stock, @Rate, @FinantialYearId, @CompanyId, @BranchId, @ApprovalStatus, @ApprovalLevel,@ApprovedBy,@ApprovalRemarks,@RejectRemarks,@IsReject, @UserId, @Action", Id, EntryDate, ProjectId, UnitId, BlockId, FloorId, DivisionId, MaterialId, Stock, Rate, FinantialYearId, CompanyId, BranchId, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovalRemarks, RejectRemarks, IsReject,userid, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<DamageStockList>> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", UserID);

                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_DamageStocklist.FromSqlRaw("Stpro_DamageStockForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<DamageStockList>> GetforEdit(int companyId, int branchid)
        {
            try { 
            var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var UserId = new SqlParameter("@UserId", "0");
           
            var Action = new SqlParameter("@Action", Actions.SelectforEdit);

            var _product = await _dbContext.tbl_DamageStocklist.FromSqlRaw("Stpro_DamageStockForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId,Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<DamageStockList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userid);

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_DamageStocklist.FromSqlRaw("Stpro_DamageStockForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<DamageStockList>> Getforview(MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                var CompanyId = new SqlParameter("@CompanyId", materialSearch. CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_DamageStocklist.FromSqlRaw("Stpro_DamageStockForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> DamageStockReport(DamageSearch damageSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_DamageStockForApproval";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(damageSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = damageSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = damageSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });
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
                return purcasedetails == string.Empty ? "[]" : purcasedetails;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetforReport(DamageStock damageStock)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseReturnMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(damageStock) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = damageStock.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = damageStock.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Select });
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


    }
}
