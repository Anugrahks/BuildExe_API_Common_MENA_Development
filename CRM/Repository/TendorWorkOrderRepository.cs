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
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Data;

namespace BuildExeServices.Repository
{
    public class TendorWorkOrderRepository:ITendorWorkOrderRepository 
    {
        private readonly ProductContext _dbContext;

        public TendorWorkOrderRepository(ProductContext dbContext)
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
        public async Task<IEnumerable<Validation>> Insert(TendorWorkOrderMaster tendorWorkOrder )
        {
            try
            {

                var Id = new SqlParameter("@Id", "0");
                var @WorkorderDate = new SqlParameter("@WorkorderDate", DateTime.Now);
                var ProjectId = new SqlParameter("@projectId", tendorWorkOrder.ProjectId );
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(tendorWorkOrder));            
                var Status = new SqlParameter("@Status", tendorWorkOrder.Status);
                if (tendorWorkOrder.StatusDescription == null)
                    tendorWorkOrder.StatusDescription = "";
                var StatusDescription = new SqlParameter("@StatusDescription", tendorWorkOrder.StatusDescription);
                var userId = new SqlParameter("@userId", tendorWorkOrder.UserId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", tendorWorkOrder.FinancialYearId);
                var CompanyId = new SqlParameter("@CompanyId", tendorWorkOrder.CompanyId);
                var BranchId = new SqlParameter("@BranchId", tendorWorkOrder.BranchId);

                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_TendorWorkOrder @Id,@WorkorderDate,@projectId,@json,@Status,@StatusDescription,@userId,@FinancialYearId,@CompanyId,@BranchId,@Action", Id, @WorkorderDate, ProjectId,  json,  Status, StatusDescription, userId, FinancialYearId, CompanyId, BranchId, Action).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(TendorWorkOrderMaster tendorWorkOrder)
        {
            try
            {
                var Id = new SqlParameter("@Id", tendorWorkOrder.Id);
                var @WorkorderDate = new SqlParameter("@WorkorderDate", DateTime.Now);
                var ProjectId = new SqlParameter("@projectId", tendorWorkOrder.ProjectId);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(tendorWorkOrder));
                var Status = new SqlParameter("@Status", tendorWorkOrder.Status);
                if (tendorWorkOrder.StatusDescription == null)
                    tendorWorkOrder.StatusDescription = "";
                var StatusDescription = new SqlParameter("@StatusDescription", tendorWorkOrder.StatusDescription);
                var userId = new SqlParameter("@userId", tendorWorkOrder.UserId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", tendorWorkOrder.FinancialYearId);
                var CompanyId = new SqlParameter("@CompanyId", tendorWorkOrder.CompanyId);
                var BranchId = new SqlParameter("@BranchId", tendorWorkOrder.BranchId);
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_TendorWorkOrder @Id,@WorkorderDate,@projectId,@json,@Status,@StatusDescription,@userId,@FinancialYearId,@CompanyId,@BranchId,@Action", Id, @WorkorderDate, ProjectId, json, Status, StatusDescription, userId, FinancialYearId, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int departmentId)
        {
            try
            {
                var department = await _dbContext.tbl_TendorWorkOrder.FindAsync(departmentId);

                _dbContext.tbl_TendorWorkOrder.Remove(department);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<TendorWorkOrder>> Get()
        {
            try
            {
                return await _dbContext.tbl_TendorWorkOrder.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<TendorWorkOrder>> GetByID(int projectid)
        {
            try
            {
                return await _dbContext.tbl_TendorWorkOrder.Where(x => x.ProjectId == projectid).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetByID2(int projectid)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_TendorWorkOrderDetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectid });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string logdetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    logdetails = logdetails + dataTable.Rows[i][0].ToString();
                }

                return logdetails;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> getBudgetedAmount(int id,int divisionId)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_TendorWorkOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@WorkorderDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@projectId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@StatusDescription", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = divisionId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string logdetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    logdetails = logdetails + dataTable.Rows[i][0].ToString();
                }

                return logdetails;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
