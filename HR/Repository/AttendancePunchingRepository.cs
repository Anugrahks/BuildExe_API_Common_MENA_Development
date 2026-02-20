using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class AttendancePunchingRepository : IAttendancePunchingRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectByEmployee = 4,
            SelectBy = 5,
            SelectList = 6
        }

        public AttendancePunchingRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<AttendancePunchingConfirm> attendances)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Dateworked = new SqlParameter("@DateWorked", DateTime.Now);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(attendances));
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AttendancePunchingConfirm @Id,@BranchId, @DateWorked,@json, @Action", materialId, BranchId, Dateworked, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> PunchingDetails(IEnumerable<HRSearch> attendances)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MobilePunchingDetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(attendances) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Insert });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<AttendancePunchingConfirm> attendances)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Dateworked = new SqlParameter("@DateWorked", DateTime.Now);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(attendances));
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AttendancePunchingConfirm @Id,@BranchId, @DateWorked,@json, @Action", materialId, BranchId, Dateworked, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int Id, int userId)
        {
            try
            {
                var materialId = new SqlParameter("@Id", Id);
                var BranchId = new SqlParameter("@BranchId", userId);
                var Dateworked = new SqlParameter("@DateWorked", DateTime.Now);
                var item = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_AttendancePunchingConfirm @Id,@BranchId, @DateWorked,@json, @Action", materialId, BranchId, Dateworked, item, Action);

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> Getdetails(int Id)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AttendancePunchingConfirm";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectBy });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetEmployee(int EmployeeId, DateTime DateWorked)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AttendancePunchingConfirm";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.DateTime) { Value = DateWorked });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByEmployee });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetListDetails(int BranchId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AttendancePunchingConfirm";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectList });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
