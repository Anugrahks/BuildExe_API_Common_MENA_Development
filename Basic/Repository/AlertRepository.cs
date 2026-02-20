using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Data.Common;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Collections;

namespace BuildExeBasic.Repository
{
    public class AlertRepository:IAlertRepository 
    {
        private readonly BasicContext _dbContext;
        public AlertRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {

            Select = 1,
            SelectCount = 2,
            Selectfew = 3
        }
        public async Task<IEnumerable<Alert> >Get(int userId, DateTime today, int companyid, int branchId)
        {
            try { 
            var UserId = new SqlParameter("@UserId", userId);
            var Today = new SqlParameter("@Today", today);
            var Companyid = new SqlParameter("@Companyid", companyid);
            var BranchId = new SqlParameter("@BranchId", branchId);
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var FromDate = new SqlParameter("@fromdate", DateTime.Now);
            var ToDate = new SqlParameter("@todate", DateTime.Now);
            var Action = new SqlParameter("@Action", Actions.Select );
            var _alert = await _dbContext.tbl_Alerts.FromSqlRaw("Stpro_Alerts @UserId,@Today,@Companyid,@BranchId,@FinancialYearId,@fromdate,@todate, @Action", 
                UserId, Today, Companyid, BranchId, FinancialYearId, FromDate,ToDate, Action).ToListAsync ();
            return _alert;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Alert>> GetwithType(int userId, DateTime today, int companyid, int branchId, int Type)
        {
            try
            {
                int actiontype = Type switch
                {
                    1 => 7,
                    2 => 8,
                    3 => 9,
                    _ => throw new ArgumentException("Invalid Type value")
                };
                var UserId = new SqlParameter("@UserId", userId);
                var Today = new SqlParameter("@Today", today);
                var Companyid = new SqlParameter("@Companyid", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var FromDate = new SqlParameter("@fromdate", DateTime.Now);
                var ToDate = new SqlParameter("@todate", DateTime.Now);
                var Action = new SqlParameter("@Action", actiontype);
                var _alert = await _dbContext.tbl_Alerts.FromSqlRaw("Stpro_Alerts @UserId,@Today,@Companyid,@BranchId,@FinancialYearId,@fromdate,@todate, @Action",
                    UserId, Today, Companyid, BranchId, FinancialYearId, FromDate, ToDate, Action).ToListAsync();
                return _alert;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetwithTypeThree(int userId, DateTime today, int companyid, int branchId, int Type, int Forms)
        {
            try
            {
                using var conn = _dbContext.Database.GetDbConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = "Stpro_Alerts";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Today", SqlDbType.DateTime) { Value = today });
                cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = Forms });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });


                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<OrderedDictionary>();

                while (await reader.ReadAsync())
                {
                    var row = new OrderedDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                        row[reader.GetName(i)] = value;
                    }
                    result.Add(row);
                }

                var orderedList = result.Select(row =>
                    row.Cast<DictionaryEntry>().ToDictionary(entry => (string)entry.Key, entry => entry.Value)
                ).ToList();

                return JsonConvert.SerializeObject(orderedList, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Alertfew>> Getfew(int userId, DateTime today, int companyid, int branchId)
        {
            try
            {
                var UserId = new SqlParameter("@UserId", userId);
                var Today = new SqlParameter("@Today", today);
                var Companyid = new SqlParameter("@Companyid", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var FromDate = new SqlParameter("@fromdate", DateTime.Now);
                var ToDate = new SqlParameter("@todate", DateTime.Now);
                var Action = new SqlParameter("@Action", Actions.Selectfew);
                var _alertFew = await _dbContext.tbl_Alertsfew.FromSqlRaw("Stpro_Alerts @UserId,@Today,@Companyid,@BranchId,@FinancialYearId,@fromdate,@todate, " +
                    "@Action", UserId, Today, Companyid, BranchId, @FinancialYearId,FromDate, ToDate, Action).ToListAsync();
                return _alertFew;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<AlertCountNew>> Getcount(int userId, DateTime today, int companyid, int branchId,int FinancialYearId)
        {
            try
            {
                var UserId = new SqlParameter("@UserId", userId);
                var Today = new SqlParameter("@Today", today);
                var Companyid = new SqlParameter("@Companyid", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var FinancialYearid = new SqlParameter("@FinancialYearId", "0");
                var FromDate = new SqlParameter("@fromdate", DateTime.Now);
                var ToDate = new SqlParameter("@todate", DateTime.Now);
                var Action = new SqlParameter("@Action", Actions.SelectCount);
                var _alertCount = await _dbContext.tbl_Alertscountnew.FromSqlRaw("Stpro_AlertCounts @UserId,@Today,@Companyid,@BranchId,@FinancialYearId,@fromdate,@todate, @Action", UserId, Today, Companyid, BranchId, FinancialYearid, FromDate, ToDate, Action).ToListAsync();
                return _alertCount;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

            public async Task<string> GetcountIndividualCount(int AlertType, int userId, DateTime today, int companyid, int branchId, int FinancialYearId)
            {
                try
                {
                    using var conn = _dbContext.Database.GetDbConnection();
                    using var cmd = conn.CreateCommand();

                    cmd.CommandText = "Stpro_Alerts";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@Today", SqlDbType.DateTime) { Value = today });
                    cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = companyid });
                    cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                    cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = AlertType });
                    cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = DateTime.Now });
                    cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = DateTime.Now });
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });


                    if (conn.State != ConnectionState.Open)
                        await conn.OpenAsync();

                    using var reader = await cmd.ExecuteReaderAsync();
                    var result = new List<OrderedDictionary>();

                    while (await reader.ReadAsync())
                    {
                        var row = new OrderedDictionary();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                            row[reader.GetName(i)] = value;
                        }
                        result.Add(row);
                    }

                    var orderedList = result.Select(row =>
                        row.Cast<DictionaryEntry>().ToDictionary(entry => (string)entry.Key, entry => entry.Value)
                    ).ToList();

                    return JsonConvert.SerializeObject(orderedList, new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                    });
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }




            public async Task<IEnumerable<AlertCount>> GetcountIonic(int userId, int companyid, int branchId)
        {
            try
            {
                var UserId = new SqlParameter("@UserId", userId);
                var Today = new SqlParameter("@Today", "1990-01-01");
                var Companyid = new SqlParameter("@Companyid", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var FromDate = new SqlParameter("@fromdate", DateTime.Now);
                var ToDate = new SqlParameter("@todate", DateTime.Now);
                var Action = new SqlParameter("@Action", "4");
                var _alertCount = await _dbContext.tbl_Alertscount.FromSqlRaw("Stpro_Alerts @UserId,@Today,@Companyid,@BranchId,@FinancialYearId,@fromdate,@todate, @Action", UserId, Today, Companyid, BranchId, FinancialYearId, FromDate, ToDate, Action).ToListAsync();
                return _alertCount;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

            public async Task<string> GetAlertForClosing(int CompanyId, int BranchId, int FinancialYearId)
            {
                try
                {
                    DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                    cmd.CommandText = "Stpro_AlertsForClosing";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = CompanyId });
                    cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                    cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });


                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }

                    DbDataReader reader = await cmd.ExecuteReaderAsync();

                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    string releasenotes = "";
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                    }
                    return releasenotes;
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }


            public async Task<string> GettodaysActivity(int userId, DateTime Todate, int companyid, int branchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_Alerts";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Today", SqlDbType.DateTime) { Value = Todate });
                cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string releasenotes = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                }
                return releasenotes;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> ActivityStatus(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_ActivityStatus";
                cmd.CommandType = CommandType.StoredProcedure;

                if (basicSearch.FromDate == null)
                {
                    basicSearch.FromDate = DateTime.Parse("1990-01-01");
                }

                if (basicSearch.ToDate == null)
                {
                    basicSearch.ToDate = DateTime.Parse("1990-01-01");
                }

                if (basicSearch.Today == null)
                {
                    basicSearch.Today = DateTime.Parse("1990-01-01");
                }

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = basicSearch.userId });
                cmd.Parameters.Add(new SqlParameter("@Today", SqlDbType.DateTime) { Value = basicSearch.Today });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string releasenotes = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                }
                return releasenotes;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> ActivityStatusSearch(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_ActivityStatus";
                cmd.CommandType = CommandType.StoredProcedure;

                if (basicSearch.FromDate == null)
                {
                    basicSearch.FromDate = DateTime.Parse("1990-01-01");
                }

                if (basicSearch.ToDate == null)
                {
                    basicSearch.ToDate = DateTime.Parse("1990-01-01");
                }

                if (basicSearch.Today == null)
                {
                    basicSearch.Today = DateTime.Parse("1990-01-01");
                }

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = basicSearch.userId });
                cmd.Parameters.Add(new SqlParameter("@Today", SqlDbType.DateTime) { Value = basicSearch.Today });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string releasenotes = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                }
                return releasenotes;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GettodaysActivityAdmin(int userId, int companyid, int branchId, DateTime fromdate, DateTime todate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_Alerts";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Today", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string releasenotes = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                }
                return releasenotes;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
