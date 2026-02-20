using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeHR.Repository
{
    public class ForemanForPaymentRepository : IForemanForPaymentRepository
    {
        private readonly HRContext _dbContext;
        public ForemanForPaymentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {

            SelectAll = 5,
            SelectforEdit = 6

        }
        public async Task<IEnumerable<ForemanForPayment>> Get(int EmployeeId, int sitemanagerid, int financialyearId, DateTime todate)
        {
            try
            {
                var billId = new SqlParameter("@billId", "0");
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var Sitemanagerid = new SqlParameter("@Sitemanagerid", sitemanagerid);
                var FinancialyearId = new SqlParameter("@FinancialyearId", financialyearId);
                var fromdate = new SqlParameter("@fromdate", "2020-01-01");
                var Todate = new SqlParameter("@Todate", todate);
                var Detail = new SqlParameter("@Detail", "1");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_ForemanForPayment.FromSqlRaw("stpro_ForemanBillForPayment @billId,@employeeId,@Sitemanagerid,@FinancialyearId,@fromdate,@Todate,@Detail, @Action", billId, employeeId, Sitemanagerid, FinancialyearId, fromdate, Todate, Detail, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ForemanForPaymentAbstract>> GetAbstract(int EmployeeId, int sitemanagerid, int financialyearId, DateTime todate)
        {
            try
            {
                var billId = new SqlParameter("@billId", "0");
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var Sitemanagerid = new SqlParameter("@Sitemanagerid", sitemanagerid);
                var FinancialyearId = new SqlParameter("@FinancialyearId", financialyearId);
                var fromdate = new SqlParameter("@fromdate", "2020-01-01");
                var Todate = new SqlParameter("@Todate", todate);
                var Detail = new SqlParameter("@Detail", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_ForemanForPaymentAbstract.FromSqlRaw("stpro_ForemanBillForPayment @billId,@employeeId,@Sitemanagerid,@FinancialyearId,@fromdate,@Todate,@Detail, @Action", billId, employeeId, Sitemanagerid, FinancialyearId, fromdate, Todate, Detail, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ForemanForPayment>> GetforEdit(int Id)
        {
            try
            {
                var billId = new SqlParameter("@billId", Id);
                var employeeId = new SqlParameter("@employeeId", "0");
                var Sitemanagerid = new SqlParameter("@Sitemanagerid", "0");
                var FinancialyearId = new SqlParameter("@FinancialyearId", "0");
                var fromdate = new SqlParameter("@fromdate", "2020-01-01");
                var Todate = new SqlParameter("@Todate", "2020-01-01");
                var Detail = new SqlParameter("@Detail", "1");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_ForemanForPayment.FromSqlRaw("stpro_ForemanBillForPayment @billId,@employeeId,@Sitemanagerid,@FinancialyearId,@fromdate,@Todate,@Detail, @Action", billId, employeeId, Sitemanagerid, FinancialyearId, fromdate, Todate, Detail, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //public async Task<IEnumerable<ForemanForPaymentAbstract>> GetforEditabstract(int Id)
        //{
        //    try
        //    {
        //        var billId = new SqlParameter("@billId", Id);
        //        var employeeId = new SqlParameter("@employeeId", "0");
        //        var Sitemanagerid = new SqlParameter("@Sitemanagerid", "0");
        //        var FinancialyearId = new SqlParameter("@FinancialyearId", "0");
        //        var fromdate = new SqlParameter("@fromdate", "2020-01-01");
        //        var Todate = new SqlParameter("@Todate", "2020-01-01");
        //        var Detail = new SqlParameter("@Detail", "0");
        //        var Action = new SqlParameter("@Action", Actions.SelectforEdit);
        //        var _product = await _dbContext.tbl_ForemanForPaymentAbstract.FromSqlRaw("stpro_ForemanBillForPayment @billId,@employeeId,@Sitemanagerid,@FinancialyearId,@fromdate,@Todate,@Detail, @Action", billId, employeeId, Sitemanagerid, FinancialyearId, fromdate, Todate, Detail, Action).ToListAsync();
        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}


        public async Task<string> GetforEditabstract(int id)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.stpro_ForemanBillForPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@billId", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Sitemanagerid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialyearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = Convert.ToDateTime("2020-01-01") });
                cmd.Parameters.Add(new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Convert.ToDateTime("2020-01-01") });
                cmd.Parameters.Add(new SqlParameter("@Detail", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectforEdit });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns if any
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue;
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        // helper
        private bool IsLikelyJson(string value)
        {
            value = value.Trim();
            return (value.StartsWith("{") && value.EndsWith("}")) ||
                   (value.StartsWith("[") && value.EndsWith("]"));
        }

        public async Task<IEnumerable<Validation>> BillValidation(int foremanId, DateTime date)
        {
            try
            {
                var id = new SqlParameter("@foremanId", foremanId);
                var fdate = new SqlParameter("@date", date);
                var validations = await _dbContext.tbl_validation.FromSqlRaw("stpro_ForemanBillValidation @foremanId,@date", id, fdate).ToListAsync();
                return validations;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
