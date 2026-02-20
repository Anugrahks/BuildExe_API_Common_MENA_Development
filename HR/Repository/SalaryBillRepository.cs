using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.ComponentModel.Design;
using BuildExeHR.Common;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BuildExeHR.Repository
{
    public class SalaryBillRepository:ISalaryBillRepository 
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            Generatebill = 6,
            SelectForPayment =7,
            SelectForEdit = 8,
            SelectForApproval = 9,
            SelectbillwithoutcurrentPayment = 10,
            SelectReportJson=11,
            GetByMonth =12
        }
        public SalaryBillRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<string> GetForApproval(int companyId, int branchId, int userId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalaryBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectAll });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string varyingHeadDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    varyingHeadDetails = varyingHeadDetails + dataTable.Rows[i][0].ToString();
                }
                return varyingHeadDetails;


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetforPayment(int EmployeeId,int companyid, int branchid, int FinanacialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalaryPaymentMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForPayment });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string salaryPaymentDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    salaryPaymentDetails = salaryPaymentDetails + dataTable.Rows[i][0].ToString();
                }
                return salaryPaymentDetails;

            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> GetSalaryBill(int companyId, int branchId, int userId, int monthId, int yearId, int financialYearId, DateTime date, int employeeId, int durationId, DateTime fromDate, DateTime toDate, int IsVariation)
        {
            try
            {
                using (DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = IsVariation != 0 ? "dbo.Stpro_SalaryBillGeneratorDuration" : "dbo.Stpro_SalaryBillGenerator";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var json = JsonConvert.SerializeObject(new { EmployeeId = employeeId, DurationId = durationId, FromDate = fromDate, ToDate = toDate });

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = financialYearId });
                    cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = json });
                    cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                    cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                    cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = monthId });
                    cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = yearId });
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = date });
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetByMonth });

                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        await cmd.Connection.OpenAsync();
                    }

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        var salaryBillDetails = new StringBuilder();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            salaryBillDetails.Append(row[0].ToString());
                        }

                        return salaryBillDetails.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> EmployeeList(int Id, int companyId, int Branchid, int UserId, int MonthId, int YearId, int FinancialYearId, DateTime Date)
        {
            try
            {
                using (DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "dbo.Stpro_SalaryBillEmployeeList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                    cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                    cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                    cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = MonthId });
                    cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = YearId });
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date });
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Id });

                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        await cmd.Connection.OpenAsync();
                    }

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        var salaryBillDetails = new StringBuilder();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            salaryBillDetails.Append(row[0].ToString());
                        }

                        return salaryBillDetails.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        
        public async Task<string> SalaryBillGenerator(HRSearch hRSearch)
        {
            try
            {
                using (DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "dbo.Stpro_SalaryBillGenerator";
                    cmd.CommandType = CommandType.StoredProcedure;
                    

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = hRSearch.FinancialYearId });
                    cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = hRSearch.MultiEmployeeId });
                    cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                    cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                    cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = hRSearch.MonthId });
                    cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = hRSearch.YearId });
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = hRSearch.UserId });
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = hRSearch.Date });
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetByMonth });

                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        await cmd.Connection.OpenAsync();
                    }

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        var salaryBillDetails = new StringBuilder();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            salaryBillDetails.Append(row[0].ToString());
                        }

                        return salaryBillDetails.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

        public async Task<string> SalaryBillReprint(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalaryBillReprint";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = DateTime.Now});
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetByMonth });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string salaryBillDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    salaryBillDetails = salaryBillDetails + dataTable.Rows[i][0].ToString();
                }
                return salaryBillDetails;


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> GetSalaryValidation(int companyId, int branchId, int userId, int monthId, int yearId, int EmployeeId,int DurationId, DateTime fromDate, DateTime toDate)
        {
                try
                {
                    var json = JsonConvert.SerializeObject(new { DurationId = DurationId, FromDate = fromDate,ToDate = toDate });
                    var materialId = new SqlParameter("@Id", EmployeeId);
                    var item = new SqlParameter("@Json", json);
                    var CompanyId = new SqlParameter("@CompanyId", companyId);
                    var BranchId = new SqlParameter("@BranchId", branchId);
                    var MonthId = new SqlParameter("@MonthId", monthId);
                    var YearId = new SqlParameter("@YearId", yearId);
                    var UserId = new SqlParameter("@UserId", userId);
                    var Action = new SqlParameter("@Action", Actions.Generatebill);
                    var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SalaryBill @Id,@Json, @CompanyId, @BranchId,@MonthId, @YearId, @UserId, @Action", materialId, item, CompanyId, BranchId, MonthId, YearId, UserId, Action).ToListAsync();
                    return purchaseList;
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                    throw;
                }
            }

        public async Task<IEnumerable<Validation>> Validation(HRSearch hRSearch)
        {
            try
            {
                var materialId = new SqlParameter("@Id", hRSearch.EmployeeId);
                var item = new SqlParameter("@Json", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var MonthId = new SqlParameter("@MonthId", hRSearch.MonthId);
                var YearId = new SqlParameter("@YearId", hRSearch.YearId);
                var UserId = new SqlParameter("@UserId", hRSearch.UserId);
                var Action = new SqlParameter("@Action", Actions.Generatebill);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SalaryBill @Id,@Json, @CompanyId, @BranchId,@MonthId, @YearId, @UserId, @Action", materialId, item, CompanyId, BranchId, MonthId, YearId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


            public async Task<IEnumerable<Validation>> Insert(IEnumerable<SalaryBill> SalaryBill)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@Json", JsonConvert.SerializeObject(SalaryBill));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var YearId = new SqlParameter("@YearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SalaryBill @Id,@Json, @CompanyId, @BranchId,@MonthId, @YearId, @UserId, @Action", materialId, item, CompanyId, BranchId,MonthId,YearId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<SalaryBill> SalaryBill)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@Json", JsonConvert.SerializeObject(SalaryBill));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var YearId = new SqlParameter("@YearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SalaryBill @Id,@Json, @CompanyId, @BranchId,@MonthId, @YearId, @UserId, @Action", materialId, item, CompanyId, BranchId, MonthId, YearId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetByUser(int companyId, int branchId, int userId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalaryBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Select });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string varyingHeadDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    varyingHeadDetails = varyingHeadDetails + dataTable.Rows[i][0].ToString();
                }
                return varyingHeadDetails;


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetEmployeeList(int CompanyId, int Branchid, int CategoryId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalaryBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = CategoryId });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string varyingHeadDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    varyingHeadDetails = varyingHeadDetails + dataTable.Rows[i][0].ToString();
                }
                return varyingHeadDetails;


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
                var Json = new SqlParameter("@Json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var YearId = new SqlParameter("@YearId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_SalaryBill @Id,@Json, @CompanyId, @BranchId,@MonthId, @YearId, @UserId, @Action", Id, Json, CompanyId, BranchId, MonthId, YearId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> Getdetailsforview(int employeeid, int monthid,int yearId, int companyid, int branchid, int financialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_SalaryBillGenerationByMonth";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value =  branchid });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = monthid });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = yearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });
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


        public async Task<string> Getdetailsforviewduration(int employeeid, int monthid,int yearId, int companyid, int branchid, int financialYearId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_SalaryBillGenerationByMonth";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = monthid });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = yearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
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


        public async Task<string> Getjson(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalaryBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });

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

        


        public async Task<string> WPSSearch(HRSearch hRSearch)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_WpsReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });

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

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
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
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> WPS(HRSearch hRSearch)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_WpsReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = hRSearch.Id });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });

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

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
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
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }



    }
}
