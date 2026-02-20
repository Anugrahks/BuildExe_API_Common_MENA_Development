using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BuildExeBasic.Repository
{
    public class PrintableReportFieldRepository : IPrintableReportFieldRepository
    {
        private readonly BasicContext _dbContext;
        public PrintableReportFieldRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PrintableReportFields>> GetByID(int MenuId)
        {
            return await _dbContext.tbl_PrintableReportFields
                .Where(x => x.MenuId == MenuId).ToListAsync();
        }

        public async Task<PurchaseOrder> GetPurchaseOrderById(int purchaseOrderId)
        {
            var id = new SqlParameter("@id", purchaseOrderId);
            var purchaseOrders = await _dbContext.PurchaseOrder.
                FromSqlRaw("Stpro_PurchaseorderById @id", id)
                .ToListAsync();
            var purchaseOrder = purchaseOrders.Any() ? purchaseOrders[0] : new PurchaseOrder();

            return purchaseOrder;
        }

        public async Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsByPurchaseOrderId(int purchaseOrderId)
        {
            var id = new SqlParameter("@id", purchaseOrderId);
            var purchaseOrders = await _dbContext.PurchaseOrderDetails.
                FromSqlRaw("Stpro_PurchaseorderDetailsById @id", id)
                .ToListAsync();
            return purchaseOrders;
        }

        public async Task<Partbill> GetPartbillbyId(int partbillid)
        {
            var id = new SqlParameter("@id", partbillid);
            var partbills = await _dbContext.Partbill.
                FromSqlRaw("Stpro_PartbillById @id", id)
                .ToListAsync();
            var partbill = partbills.Any() ? partbills[0] : new Partbill();

            return partbill;
        }

        public async Task<IEnumerable<PartbillDetails>> GetPartbilldetailsByPartbillId(int partbillid)
        {
            var id = new SqlParameter("@id", partbillid);
            var partbills = await _dbContext.PartbillDetails.
                FromSqlRaw("Stpro_PartBillDetailsById @id", id)
                .ToListAsync();
            return partbills;
        }

        //     public async Task<IEnumerable<printablekeyvalue>> GetPrintableById(int id, int Menuid)
        //  {
        //       var Id = new SqlParameter("Id", id);
        //      var MenuId = new SqlParameter("@MenuId", Menuid);
        //     var Action = new SqlParameter("@action", Actions.Selectbyid);
        //    var _printable = await _dbContext.tbl_printablebyid.FromSqlRaw("Stpro_PrintableReportById @Id, @MenuId, @action", Id, MenuId, Action).ToListAsync();
        //    if (_printable.Any())
        //   {
        //      return _printable;
        //   }
        //  else
        // {
        //     return Enumerable.Empty<printablekeyvalue>();
        // }

        public async Task<string> GetPrintableById(int id, int Menuid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PrintableReportById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.NVarChar) { Value = Menuid });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.NVarChar) { Value = 0 });
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

        public async Task<string> GetPrintableByIdDivision(int id,int DivisionId, int Menuid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PrintableReportById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.NVarChar) { Value = Menuid });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.NVarChar) { Value = DivisionId });
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


        
        public async Task<string> GetsalaryslipById(DateTime SalaryBillDate, int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalarySlipPrintable";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = MonthId });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = YearId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@SalaryBillDate", SqlDbType.DateTime) { Value = SalaryBillDate });
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


        public async Task<string> Getforsalaryslipduration(DateTime SalaryBillDate, int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId, int DurationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SalarySlipPrintableDuration";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = MonthId });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = YearId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@SalaryBillDate", SqlDbType.DateTime) { Value = SalaryBillDate });
                cmd.Parameters.Add(new SqlParameter("@DurationId", SqlDbType.Int) { Value = DurationId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate });
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

        


        public async Task<string> IndividualEntryPrint(IndividualPrintable individual)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_IndividualEntryPrintable";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(individual) });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = individual.MenuId });

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
