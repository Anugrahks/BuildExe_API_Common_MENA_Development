using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BuildExeServices.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly ProductContext _dbContext;
        public DivisionRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Get = 4,
            InsertDivision=5,
            UpdateDivision=8,
        }

        public async Task<string> Get(int companyid, int branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Get });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetDefault(int companyid, int branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetProject(int ProjectId, int Branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetProjectall (int ProjectId, int Branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 12 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }




        public async Task<string> GetProjectStatus(int ProjectId, int Branchid, int Status)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = Status });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<Division> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Division.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Division>> Get()
        {
            try
            {
                return await _dbContext.tbl_Division.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //Dont use same action for two repository files. Remove this Api. Adil Please Check --Rohith
        public async Task<IEnumerable<Division>> Getuser(int companyid, int branchid, int UserId)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var Userid = new SqlParameter("@UserId", UserId);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", 4);

                return await _dbContext.tbl_Division.FromSqlRaw("Stpro_Division @id,@json, @CompanyId, @BranchId, @UserId, @Action", Id, Userid, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> InsertDivision(DivisionProject divisionProject)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(divisionProject));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.InsertDivision);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Division @id,@json, @CompanyId, @BranchId, @UserId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> UpdateDivision(DivisionProject divisionProject)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(divisionProject));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.UpdateDivision);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Division @id,@json, @CompanyId, @BranchId, @UserId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(Division enquiryStatus)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(enquiryStatus));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Division @id,@json, @CompanyId, @BranchId, @UserId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Delete(int id, int userId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Delete });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }
                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<IEnumerable<Validation>> Update(Division enquirystatus)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(enquirystatus));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Division @id,@json, @CompanyId, @BranchId, @UserId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(int divisionId)
        {
            try
            {
                var Id = new SqlParameter("@Id", divisionId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckDivsntEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
