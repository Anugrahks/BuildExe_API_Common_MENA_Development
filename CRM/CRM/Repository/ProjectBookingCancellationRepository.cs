using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public class ProjectBookingCancellationRepository:IProjectBookingCancellationRepository 
    {
        private readonly ProductContext _dbContext;
        public ProjectBookingCancellationRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            ProjectCancelReport = 2,
        }

        public async Task Insert(ProjectBookingCancellation projectBookingCancellation )
        {
            try
            {

                if (projectBookingCancellation.RefNO  == null)
                    projectBookingCancellation.RefNO = "";
                if (projectBookingCancellation.BankId == null)
                    projectBookingCancellation.BankId = 0;

                if (projectBookingCancellation.PaymentDate == null)
                    projectBookingCancellation.PaymentDate = new DateTime(1999, 01, 01);

                var Id = new SqlParameter("@Id", "0");

                var UnitId = new SqlParameter("@UnitId", projectBookingCancellation.UnitId);
                var CancellationDate = new SqlParameter("@CancellationDate", projectBookingCancellation.CancellationDate);
                var BookingAmount = new SqlParameter("@BookingAmount", projectBookingCancellation.BookingAmount);
                var Deduction = new SqlParameter("@Deduction", projectBookingCancellation.Deduction);
                var RefundAmount = new SqlParameter("@RefundAmount", projectBookingCancellation.RefundAmount);

                var BankId = new SqlParameter("@BankId", projectBookingCancellation.BankId);
                var RefNO = new SqlParameter("@RefNO", projectBookingCancellation.RefNO);
               
                var CompanyId = new SqlParameter("@CompanyId", projectBookingCancellation.CompanyId);
                var BranchId = new SqlParameter("@BranchId", projectBookingCancellation.BranchId);
                var UserId = new SqlParameter("@UserId", projectBookingCancellation.UserId);
                
                var FinancialYearId = new SqlParameter("@FinancialYearId", projectBookingCancellation.FinancialYearId);

                var PaymentDate = new SqlParameter("@PaymentDate", projectBookingCancellation.PaymentDate);
                var PaymentMode = new SqlParameter("@PaymentMode", projectBookingCancellation.PaymentMode);
                var Action = new SqlParameter("@Action", Actions.Insert);

                await _dbContext.Database.ExecuteSqlRawAsync("stpro_ProjectBookingCancellation @Id, @UnitId, @CancellationDate, @BookingAmount, @Deduction, @RefundAmount, @BankId, @RefNO, @CompanyId, @BranchId,@UserId, @FinancialYearId,@PaymentDate,@PaymentMode,@Action", Id, UnitId, CancellationDate, BookingAmount, Deduction, RefundAmount, BankId, RefNO, CompanyId, BranchId, UserId, FinancialYearId, PaymentDate, PaymentMode, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDeletedClients()
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetDeletedClientDetails";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
            catch(Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectCancelReport(ProjectBookingCancelFilter filter)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectCancelReport";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(filter) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = filter.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = filter.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectCancelReport });
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
    }
}
