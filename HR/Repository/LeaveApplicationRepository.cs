using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeHR.Repository
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectforAccountClearence = 6,
            GetLastLEave = 7,
            DateValidation =8

        }
        public LeaveApplicationRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<LeaveApplication> LeaveApplication)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var x = JsonConvert.SerializeObject(LeaveApplication);
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(LeaveApplication));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LeaveApplication>> GetbyID(int Idworkorder)
        {
            try
            {

                var list = await _dbContext.tbl_LeaveApplication.Where(x => x.Id == Idworkorder).Where(x => x.IsDeleted == 0).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetdetailsbyID(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_LeaveApplication";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<LeaveApplication> LeaveApplication)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var x = JsonConvert.SerializeObject(LeaveApplication);
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(LeaveApplication));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int id, int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LeaveApplicationList>> GetforEdit(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_LeaveApplicationList.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LeaveApplicationList>> GetforApproval(int companyid, int branchid, int Userid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", Userid);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);
                var _product = await _dbContext.tbl_LeaveApplicationList.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LeaveApplicationList>> GetforAccountClearence(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforAccountClearence);
                var _product = await _dbContext.tbl_LeaveApplicationList.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LeaveApplicationList>> GetLastLeave(int Employeeid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Employeeid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.GetLastLEave);
                var _product = await _dbContext.tbl_LeaveApplicationList.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LeaveApplicationDocument>> GetleaveDocuments(int Id)
        {
            try
            {
                // List<ForemanWorkBill> nestedclass = new List<ForemanWorkBill>();

                var detaillist1 = await _dbContext.tbl_LeaveApplicationDocument.Where(x => x.LeaveApplicationId == Id).ToListAsync();
                return detaillist1;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Datevalidation(DateTime fromdate, int employeeid)
        {
            try
            {
                var date = new SqlParameter("@FromDate", fromdate);
                var EmployeeId = new SqlParameter("@EmployeeId", employeeid);
                var LeaveId = new SqlParameter("@LeaveId", "0");
                var Userid = new SqlParameter("@UserId", "0");
                var durationId = new SqlParameter("@durationId", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", 1);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ValidationsinPayroll @FromDate,@EmployeeId,@LeaveId,@UserId,@durationId, @json, @Action", date, EmployeeId, LeaveId, Userid, durationId,json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> LeaveValidation(DateTime fromdate, int employeeid, int leaveid, int financialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinPayroll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.Date) { Value = fromdate });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = leaveid });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@durationId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 2 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> LeaveValidationDuration(DateTime fromdate, int employeeid, int leaveid, int financialYearId, int DurationId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinPayroll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.Date) { Value = fromdate });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = leaveid });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@durationId", SqlDbType.Int) { Value = DurationId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 6 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Status(int CompanyId, int BranchId, int Category, string status)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinPayroll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = Category });
                cmd.Parameters.Add(new SqlParameter("@durationId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 7 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        
        public async Task<string> LeaveValidationmonthly(int monthid, int yearid, int employeeid, int leaveid, int financialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinMonthlyandOthers";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@monthid", SqlDbType.Int) { Value = monthid});
                cmd.Parameters.Add(new SqlParameter("@yearid", SqlDbType.Int) { Value = yearid });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = leaveid });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 1 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> LeaveValidationmonthlyDuration(int monthid, int yearid, int employeeid, int leaveid, int financialYearId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinMonthlyandOthers";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@monthid", SqlDbType.Int) { Value = monthid });
                cmd.Parameters.Add(new SqlParameter("@yearid", SqlDbType.Int) { Value = yearid });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = leaveid });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 2 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> LeaveApplication(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_LeaveApplication";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 9 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<LeaveApplicationList>> GetforEditMobile(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_LeaveApplicationList.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<Validation>> DatevalidationMobile(DateTime fromdate, int employeeid)
        {
            try
            {
                var date = new SqlParameter("@FromDate", fromdate);
                var EmployeeId = new SqlParameter("@EmployeeId", employeeid);
                var LeaveId = new SqlParameter("@LeaveId", "0");
                var Userid = new SqlParameter("@UserId", "0");
                var durationId = new SqlParameter("@durationId", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", 1);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ValidationsinPayroll @FromDate,@EmployeeId,@LeaveId,@UserId,@durationId, @json, @Action", date, EmployeeId, LeaveId, Userid, durationId, json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> InsertMobile(IEnumerable<LeaveApplication> LeaveApplication)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var x = JsonConvert.SerializeObject(LeaveApplication);
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(LeaveApplication));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LeaveApplication @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> LeaveValidationDurationMobile(DateTime fromdate, int employeeid, int leaveid, int financialYearId, int DurationId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinPayroll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.Date) { Value = fromdate });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = leaveid });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@durationId", SqlDbType.Int) { Value = DurationId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 6 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> AppliedAnnualLeaves(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetsForAnnualLeave";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = hRSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> DetailsForAnnLv(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetsForAnnualLeave";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetEmployeeSettlements(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetsForAnnualLeave";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = hRSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetSalaryHeads(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetsForAnnualLeave";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = hRSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
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
                if (details == "")
                    details = "[]";
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
