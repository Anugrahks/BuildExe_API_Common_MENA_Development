using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;

using Microsoft.Data.SqlClient;

using Newtonsoft.Json;
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace BuildExeServices.Repository
{
    public class TendorAnalysisRepository:ITendorAnalysisRepository 
    {
        private readonly ProductContext _dbContext;

        public TendorAnalysisRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,

        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<TendorAnalysis> tendorAnalysis)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(tendorAnalysis));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_TendorAnalysis @Id,@projectId,@item,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<TendorAnalysis> tendorAnalysis)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(tendorAnalysis));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var result= await _dbContext.tbl_validation.FromSqlRaw("stpro_TendorAnalysis @Id,@projectId,@item,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return result;
                // _dbContext.Entry(tendorAnalysis).State = EntityState.Modified;
                //  await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int projectId,int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", projectId);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                await _dbContext.Database.ExecuteSqlRawAsync("stpro_TendorAnalysis @Id,@projectId,@item,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, item, CompanyId, BranchId, userId, Action);

                //var department = await _dbContext.tbl_TenderAnalysis.FindAsync(tId);

                // _dbContext.tbl_TenderAnalysis.Remove(department);
                //  await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<TendorAnalysis >> Get()
        {
            try
            {
                return await _dbContext.tbl_TenderAnalysis.ToListAsync();
            }
            catch (Exception)
            { throw; }
        }
        public async Task<IEnumerable<TendorAnalysis>> GetByID(int projectid)
        {
            try
            {
                return await _dbContext.tbl_TenderAnalysis.Where(x => x.ProjectId == projectid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetReport(BillSearch billSearch)
        {
            try
            {
                //    var Id = new SqlParameter("@Id", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                //var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", billSearch.BranchId );
                //var userId = new SqlParameter("@userId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectforReport );

                //var _product = await _dbContext.tbl_PartBillMasterReport.FromSqlRaw("Stpro_PartBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                //return _product;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_TendorAnalysis";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@projectId", SqlDbType.NVarChar) { Value = 0});
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(billSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = billSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = billSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
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

        public async Task<string> GetGovReport(BillSearch billSearch)
        {
            try
            {
                //    var Id = new SqlParameter("@Id", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                //var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", billSearch.BranchId );
                //var userId = new SqlParameter("@userId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectforReport );

                //var _product = await _dbContext.tbl_PartBillMasterReport.FromSqlRaw("Stpro_PartBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                //return _product;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_TendorAnalysis";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@projectId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(billSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = billSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = billSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
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

        public async Task<string> GetEmdReport(BillSearch billSearch)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_TendorAnalysis";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@projectId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(billSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = billSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = billSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });
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
