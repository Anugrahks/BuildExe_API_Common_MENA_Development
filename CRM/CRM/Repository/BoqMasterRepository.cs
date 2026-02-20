using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class BoqMasterRepository: IBoqMasterRepository
    {
        private readonly ProductContext _dbContext;
        public BoqMasterRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            getforEdit = 6,
            getforapproval = 7,
            getOneDetails = 8,
            validation=9,
            Getformasapproval=10,
            UpdateMAsStatus = 11

        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<BoqMaster> boqMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(boqMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<BoqMaster> boqMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(boqMasters));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Update );

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int id, int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", userid);
            var Action = new SqlParameter("@Action", Actions.Delete);

            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<BoqMaster >> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_BoqMaster .Where(x => x.Id == Idworkorder).ToListAsync();
            var detaillist = await _dbContext.tbl_BoqDetails.Where(x => x.BoqMasterId  == Idworkorder).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<BoqMasterList>> GetForEdit(int companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", Branchid);
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.getforEdit );

            var _product = await _dbContext.tbl_BoqMasterList.FromSqlRaw("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync ();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> GetForEdituser(int companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 13 });
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


        public async Task<string> getuserSteel(int companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 25 });
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

        

        public async Task<IEnumerable<BoqMasterList>> GetForApproval(int companyid, int Branchid,int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", Branchid);
            var userId = new SqlParameter("@userId", UserId);
            var Action = new SqlParameter("@Action", Actions.getforapproval);

            var _product =await  _dbContext.tbl_BoqMasterList.FromSqlRaw("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<BoqDetailsList>> GetDetails(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.getOneDetails);

                var _product = await  _dbContext.tbl_BoqDetailsList.FromSqlRaw("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> GetValidation(int Projectid, int UnitId,int Blockid,int FloorId,int WorkNameid,int id)
        {
            try
            {

                var Id = new SqlParameter("@Id", id);
                var projectId = new SqlParameter("@ProjectId", Projectid);
                var unitid = new SqlParameter("@unitid", UnitId);
                var blockid = new SqlParameter("@blockid", Blockid);
                var floorid = new SqlParameter("@floorid", FloorId);
                var workid = new SqlParameter("@workid", WorkNameid);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.validation);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Boqvalidation @Id,@projectId, @unitid, @blockid, @floorid, @workid,@CompanyId,@BranchId,@userId,@Action", Id, projectId, unitid, blockid, floorid, workid, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFor_MasApproval(int  ProjectId)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_BoqMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value =0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value =0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Getformasapproval  });
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
        public async Task Update_MAsStatus(BillSearch billSearch )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.UpdateMAsStatus);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_BoqMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action);
              
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
