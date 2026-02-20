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
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BuildExeHR.Repository
{
    public class TableAttendanceRepository : ITableAttendanceRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Delete =3,
            SelectForedit = 4

        }

        public TableAttendanceRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<TableAttendance> attendances)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var projectId = new SqlParameter("@ProjectId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var dateworked = new SqlParameter("@DateWorked", DateTime.Now);
                var Todate = new SqlParameter("@Todate", DateTime.Now);
                var IsGroup = new SqlParameter("@isGroup", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_TableAttendance @Id,@ProjectId, @item,@CompanyId,@BranchId,@DateWorked,@Todate,@isGroup,@Action", id, projectId, item, CompanyId, BranchId, dateworked, Todate, IsGroup, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
  /*      public async Task<string> GetForEdit(int companyid, int branchid, int EmployeeId, string Dateworked)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_TableAttendance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@item", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.VarChar) { Value = Dateworked });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForedit });
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
  */
        public async Task<IEnumerable<TableAttendanceGet>> GetForEdit(int companyid, int branchid, int EmployeeId, string Dateworked)
        {
            try
            {
                var employeeId = new SqlParameter("@Id", EmployeeId);
                var projectId = new SqlParameter("@ProjectId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var dateworked = new SqlParameter("@DateWorked", Dateworked);
                var Todate = new SqlParameter("@Todate", DateTime.Now);
                var IsGroup = new SqlParameter("@isGroup", "0");
                var action = new SqlParameter("@Action", Actions.SelectForedit);

                var _employee = await _dbContext.tbl_TableAttendanceget.FromSqlRaw("Stpro_TableAttendance @Id,@ProjectId, @item,@CompanyId,@BranchId,@DateWorked,@Todate,@isGroup,@Action", employeeId, projectId, item, CompanyId, BranchId, dateworked, Todate, IsGroup, action).ToListAsync();


                return _employee;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(string fromdate, string todate, int projectid, int employeeid, int companyId, int branchId, int isgroup)
        {
            try
            {
                var employeeId = new SqlParameter("@Id", employeeid);
                var projectId = new SqlParameter("@ProjectId", projectid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var dateworked = new SqlParameter("@DateWorked", fromdate);
                var Todate = new SqlParameter("@Todate", todate);
                var IsGroup = new SqlParameter("@isGroup", isgroup);
                var action = new SqlParameter("@Action", Actions.Delete);

                var _employee = await _dbContext.tbl_validation.FromSqlRaw("Stpro_TableAttendance @Id,@ProjectId, @item,@CompanyId,@BranchId,@DateWorked,@Todate,@isGroup,@Action", employeeId, projectId, item, CompanyId, BranchId, dateworked,Todate, IsGroup, action).ToListAsync();


                return _employee;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}