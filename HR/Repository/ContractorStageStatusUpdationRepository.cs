using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;

namespace BuildExeHR.Repository
{
    public class ContractorStageStatusUpdationRepository : IContractorStageStatusUpdationRepository
    {
        private readonly HRContext _dbContext;

        public enum Actions
        {
            Insert=1,
            Update=2
        }
        public ContractorStageStatusUpdationRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ContractorStageSetting> ContractorStage)
        {
            try
            {
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(ContractorStage));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var id = new SqlParameter("@Id", "0");
                var Action = new SqlParameter("@Action", 2);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorStageStatusUpdation @json, @CompanyId,@BranchId, @Id,@Action",item, CompanyId, BranchId, id, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<Validation>> ContractorAdditionalBill(IEnumerable<ContractorStageSetting> ContractorStage)
        {
            try
            {
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(ContractorStage));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var id = new SqlParameter("@Id", "0");
                var Action = new SqlParameter("@Action", 2);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorAdditionalBill @json, @CompanyId,@BranchId, @Id,@Action", item, CompanyId, BranchId, id, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        
        public async Task<string> GetbyAdditionalBill(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ContractorAdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getbyid(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ContractorStageStatusUpdation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ContractorAdditionalbill> contractorAdditionalbill)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(contractorAdditionalbill));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorAdditionalbill @Id,@json,@CompanyId,@BranchId,@FinancialYearId,@userId,@Action", Id, item, CompanyId, BranchId, FinancialYearId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Additionalbill(IEnumerable<ContractorAdditionalbill> contractorAdditionalbill)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(contractorAdditionalbill));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorAdditionalbill @Id,@json,@CompanyId,@BranchId,@FinancialYearId,@userId,@Action", Id, item, CompanyId, BranchId, FinancialYearId, userId, Action).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetAdditionalBill(int CompanyId, int Branchid, int FinacialYearId, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ContractorAdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetByIdAdditionalBill(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ContractorAdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetApprovalAdditionalBill(int CompanyId, int Branchid, int FinacialYearId, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ContractorAdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDeleteAdditionalBill(int Id, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ContractorAdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }

}
