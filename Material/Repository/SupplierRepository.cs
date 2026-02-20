using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.DBContexts;
using Microsoft.Data.SqlClient;
using BuildExeMaterialServices.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly MaterialContext _dbContext;

        public SupplierRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            checkTransaction = 6
        }
        public async Task<IEnumerable<Validation>> Insert(Supplier supplier)
        {
            try
            {
                var Id = new SqlParameter("@Id","0");
            var SupplierId = new SqlParameter("@SupplierId", "0");
            var SupId = new SqlParameter("@SupId", supplier.SupId);
            var SupplierName = new SqlParameter("@SupplierName", supplier.SupplierName );
            var SupplierAddres1 = new SqlParameter("@SupplierAddres1", supplier.SupplierAddres1);
            var SupplierAddres2 = new SqlParameter("@SupplierAddres2", supplier.SupplierAddres2);
            var Post = new SqlParameter("@Post", supplier.Post);
            var Pin = new SqlParameter("@Pin", supplier.Pin);
            var PhoneNumber = new SqlParameter("@PhoneNumber", supplier.PhoneNumber);
            var MobileNumber = new SqlParameter("@MobileNumber", supplier.MobileNumber);
            var EmailId = new SqlParameter("@EmailId", supplier.EmailId );
            var TINNo = new SqlParameter("@TINNo", supplier.TINNo );
            var GSTNo = new SqlParameter("@GSTNo", supplier.GSTNo );
            var CompanyId = new SqlParameter("@CompanyId", supplier.CompanyId );
            var BranchId = new SqlParameter("@BranchId", supplier.BranchId );
            var FinancialYearId = new SqlParameter("@FinancialYearId", supplier.FinancialYearId);
            var OpeningType = new SqlParameter("@OpeningType", supplier.OpeningType );
            var OpeningBalance = new SqlParameter("@OpeningBalance", supplier.OpeningBalance );
            var OpeningBalanceRecover = new SqlParameter("@OpeningBalanceRecover", supplier.OpeningBalanceRecover );
            var UserId = new SqlParameter("@UserId", supplier.UserId);
                var BlackListed = new SqlParameter("@BlackListed", supplier.BlackListed);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(supplier));
                var Action = new SqlParameter("@Action",Actions.Insert);

                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierMaster @Id, @SupplierId, @SupId, @SupplierName, @SupplierAddres1, @SupplierAddres2, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TINNo, @GSTNo, @CompanyId, @BranchId, @FinancialYearId, @OpeningType, @OpeningBalance, @OpeningBalanceRecover,@UserId,@BlackListed,@json,@Action", Id, SupplierId, SupId, SupplierName, SupplierAddres1, SupplierAddres2, Post, Pin, PhoneNumber, MobileNumber, EmailId, TINNo, GSTNo, CompanyId, BranchId, FinancialYearId, OpeningType, OpeningBalance, OpeningBalanceRecover, UserId, BlackListed, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(Supplier supplier)
        {
            try
            {
                var Id = new SqlParameter("@Id", supplier.Id );
            var SupplierId = new SqlParameter("@SupplierId", supplier.SupplierId );
                var SupId = new SqlParameter("@SupId", supplier.SupId);
                var SupplierName = new SqlParameter("@SupplierName", supplier.SupplierName);
            var SupplierAddres1 = new SqlParameter("@SupplierAddres1", supplier.SupplierAddres1);
            var SupplierAddres2 = new SqlParameter("@SupplierAddres2", supplier.SupplierAddres2);
            var Post = new SqlParameter("@Post", supplier.Post);
            var Pin = new SqlParameter("@Pin", supplier.Pin);
            var PhoneNumber = new SqlParameter("@PhoneNumber", supplier.PhoneNumber);
            var MobileNumber = new SqlParameter("@MobileNumber", supplier.MobileNumber);
            var EmailId = new SqlParameter("@EmailId", supplier.EmailId);
            var TINNo = new SqlParameter("@TINNo", supplier.TINNo);
            var GSTNo = new SqlParameter("@GSTNo", supplier.GSTNo);
            var CompanyId = new SqlParameter("@CompanyId", supplier.CompanyId);
            var BranchId = new SqlParameter("@BranchId", supplier.BranchId);
            var FinancialYearId = new SqlParameter("@FinancialYearId", supplier.FinancialYearId);
            var OpeningType = new SqlParameter("@OpeningType", supplier.OpeningType);
            var OpeningBalance = new SqlParameter("@OpeningBalance", supplier.OpeningBalance);
            var OpeningBalanceRecover = new SqlParameter("@OpeningBalanceRecover", supplier.OpeningBalanceRecover);
            var UserId = new SqlParameter("@UserId", supplier.UserId);
                var BlackListed = new SqlParameter("@BlackListed", supplier.BlackListed);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(supplier));
                var Action = new SqlParameter("@Action", Actions.Update);

                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierMaster @Id, @SupplierId, @SupId, @SupplierName, @SupplierAddres1, @SupplierAddres2, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TINNo, @GSTNo, @CompanyId, @BranchId, @FinancialYearId, @OpeningType, @OpeningBalance, @OpeningBalanceRecover,@UserId,@BlackListed, @json,@Action", Id, SupplierId, SupId, SupplierName, SupplierAddres1, SupplierAddres2, Post, Pin, PhoneNumber, MobileNumber, EmailId, TINNo, GSTNo, CompanyId, BranchId, FinancialYearId, OpeningType, OpeningBalance, OpeningBalanceRecover, UserId, BlackListed, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetSuppliers(int CompanyId, int BranchId, int IsServiceCreditors)
        {
            try
            {
                //string json = JsonConvert.SerializeObject(IsServiceCreditors);

                string json = $"{{\"IsServiceCreditors\": {IsServiceCreditors}}}";

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SupplierPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
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

        public async Task<string> Getwithfinancial(int CompanyId, int BranchId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SupplierPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
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


        public async Task<string> Get(int CompanyId, int BranchId, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SupplierPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
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


        public async Task<string> GetReport(int CompanyId, int BranchId, int Reportid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SupplierPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Reportid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
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

        public async Task<Supplier> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Suppliers.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int id,int Userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var SupId = new SqlParameter("@SupId", "");
                var SupplierName = new SqlParameter("@SupplierName", "");
                var SupplierAddres1 = new SqlParameter("@SupplierAddres1", "");
                var SupplierAddres2 = new SqlParameter("@SupplierAddres2", "");
                var Post = new SqlParameter("@Post", "");
                var Pin = new SqlParameter("@Pin", "");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "");
                var MobileNumber = new SqlParameter("@MobileNumber", "");
                var EmailId = new SqlParameter("@EmailId", "");
                var TINNo = new SqlParameter("@TINNo", "");
                var GSTNo = new SqlParameter("@GSTNo", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var OpeningType = new SqlParameter("@OpeningType", "");
                var OpeningBalance = new SqlParameter("@OpeningBalance", "0");
                var OpeningBalanceRecover = new SqlParameter("@OpeningBalanceRecover", "0");
                var UserId = new SqlParameter("@UserId", Userid);
                var BlackListed = new SqlParameter("@BlackListed", "0");
                var json = new SqlParameter("@json", "{}");
                var Action = new SqlParameter("@Action", Actions.Delete);

                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierMaster @Id, @SupplierId, @SupId, @SupplierName, @SupplierAddres1, @SupplierAddres2, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TINNo, @GSTNo, @CompanyId, @BranchId, @FinancialYearId, @OpeningType, @OpeningBalance, @OpeningBalanceRecover,@UserId,@BlackListed, @json, @Action", Id, SupplierId, SupId, SupplierName, SupplierAddres1, SupplierAddres2, Post, Pin, PhoneNumber, MobileNumber, EmailId, TINNo, GSTNo, CompanyId, BranchId, FinancialYearId, OpeningType, OpeningBalance, OpeningBalanceRecover, UserId, BlackListed, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckTransactonExists(int supplierid)
        {
            try
            {
                var Id = new SqlParameter("@Id", supplierid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var SupId = new SqlParameter("@SupId", "");
                var SupplierName = new SqlParameter("@SupplierName", "");
                var SupplierAddres1 = new SqlParameter("@SupplierAddres1", "");
                var SupplierAddres2 = new SqlParameter("@SupplierAddres2", "");
                var Post = new SqlParameter("@Post", "");
                var Pin = new SqlParameter("@Pin", "");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "");
                var MobileNumber = new SqlParameter("@MobileNumber", "");
                var EmailId = new SqlParameter("@EmailId", "");
                var TINNo = new SqlParameter("@TINNo", "");
                var GSTNo = new SqlParameter("@GSTNo", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var OpeningType = new SqlParameter("@OpeningType", "");
                var OpeningBalance = new SqlParameter("@OpeningBalance", "0");
                var OpeningBalanceRecover = new SqlParameter("@OpeningBalanceRecover", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var BlackListed = new SqlParameter("@BlackListed", "0");
                var json = new SqlParameter("@json", "{}");
                var Action = new SqlParameter("@Action", Actions.checkTransaction);

                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierMaster @Id, @SupplierId, @SupId, @SupplierName, @SupplierAddres1, @SupplierAddres2, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TINNo, @GSTNo, @CompanyId, @BranchId, @FinancialYearId, @OpeningType, @OpeningBalance, @OpeningBalanceRecover,@UserId,@BlackListed, @json,@Action", Id, SupplierId, SupId, SupplierName, SupplierAddres1, SupplierAddres2, Post, Pin, PhoneNumber, MobileNumber, EmailId, TINNo, GSTNo, CompanyId, BranchId, FinancialYearId, OpeningType, OpeningBalance, OpeningBalanceRecover, UserId, BlackListed, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int supplierid)
        {
            try
            {
                var Id = new SqlParameter("@Id", supplierid);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckSupplierRegEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetSuppplierQuotation(int ProjectId, int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SupplierPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
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
    }
}
