using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
namespace BuildExeBasic.Repository
{
    public class AccountHeadRepository : IAccountHeadRepository
    {
        private readonly BasicContext _dbContext;
        public AccountHeadRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            SelectByType = 6,
            Selectjournal = 7,
            SelectBalance = 8,
            GetAllHeads = 9,
            SelectByTypeForLedgermerging = 10,
            ByBranch=11,
            GetAllWithSuppliers=12
        }
        public async Task Delete(int ID, int UserId)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", ID);
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var userId = new SqlParameter("@UserId", UserId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AccountHead>> GetByID(int iD)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", iD);
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AccountHead>> Get(int companyId, int branchid)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AccountHead>> Getuser(int companyId, int branchid, int UserId)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var Userid = new SqlParameter("@UserId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, Userid, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AccountHead>> GetAll(int companyId, int branchid)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.GetAllHeads);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AccountHead>> GetWithSuppliers(int companyId, int branchid)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.GetAllWithSuppliers);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<AccountHead>> Getjournal(int companyId, int branchid)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectjournal);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetdebitCredit(int creditid, int debitid, int financialyearid)
        {
            string amt = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.stpro_Balancingjournal";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@creditid", SqlDbType.VarChar) { Value = creditid });
                cmd.Parameters.Add(new SqlParameter("@debitid", SqlDbType.VarChar) { Value = debitid});
                cmd.Parameters.Add(new SqlParameter("@financialyearid", SqlDbType.VarChar) { Value = financialyearid });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string balance = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    balance = balance + dataTable.Rows[i][0].ToString();
                }
                balance.Replace("{[", "").Replace("]}", "");
                return balance;

                //amt = dataTable.Rows[0][0].ToString();

            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                amt = "0";
            }
            return amt;

        }

        public async Task<IEnumerable<AccountHead>> GetDetails(int AccountHeadId, int CompanyId, int BranchId)
        {
            try
            {
                var accountHeadId = new SqlParameter("@AccountHeadId", AccountHeadId);
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectjournal);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetAdvancepayment(int AccountHeadId, int SupplierId, int Type, int CategoryId, int JournalType, int ProjectId, int financialyearid, int Id)
        {
            string amt = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.stpro_SupplierList";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@AccountHeadId", SqlDbType.Int) { Value = AccountHeadId });
                cmd.Parameters.Add(new SqlParameter("@SupplierId", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = Type });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = CategoryId });
                cmd.Parameters.Add(new SqlParameter("@JournalType", SqlDbType.Int) { Value = JournalType });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = financialyearid });
                cmd.Parameters.Add(new SqlParameter("@EntryId", SqlDbType.Int) { Value = Id });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string balance = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    balance = balance + dataTable.Rows[i][0].ToString();
                }
                balance.Replace("{[", "").Replace("]}", "");
                return balance;

                //amt = dataTable.Rows[0][0].ToString();

            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                amt = "0";
            }
            return amt;

        }



        public async Task<IEnumerable<AccountHead>> Get(int companyId, int branchid, int accountTypeId)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", accountTypeId);
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectByType);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> Insert(AccountHead accountHead)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "1");
                var AccountHeadName = new SqlParameter("@AccountHeadName", accountHead.AccountHeadName);
                var AccountTypeId = new SqlParameter("@AccountTypeId", accountHead.AccountTypeId);
                var AccountGroupId = new SqlParameter("@AccountGroupId", accountHead.AccountGroupId);
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId", accountHead.AccountSubGroupId ?? (object)DBNull.Value);
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", accountHead.AccountSubGroupName);
                var CompanyId = new SqlParameter("@CompanyId", accountHead.CompanyId);
                var BranchId = new SqlParameter("@BranchId", accountHead.BranchId);
                var OpeningAmount = new SqlParameter("@OpeningAmount", accountHead.OpeningAmount);
                var OpeningType = new SqlParameter("@OpeningType", accountHead.OpeningType);
                var Description = new SqlParameter("@Description", accountHead.Description);
                var Editable = new SqlParameter("@Editable", accountHead.Editable);
                var financialyearId = new SqlParameter("@financialyearId", accountHead.financialyearId);
                var UserId = new SqlParameter("@UserId", accountHead.UserId);
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(AccountHead accountHead)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", accountHead.AccountHeadId);
                var AccountHeadName = new SqlParameter("@AccountHeadName", accountHead.AccountHeadName);
                var AccountTypeId = new SqlParameter("@AccountTypeId", accountHead.AccountTypeId);
                var AccountGroupId = new SqlParameter("@AccountGroupId", accountHead.AccountGroupId);
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", accountHead.AccountSubGroupId);
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", accountHead.AccountSubGroupName);
                var CompanyId = new SqlParameter("@CompanyId", accountHead.CompanyId);
                var BranchId = new SqlParameter("@BranchId", accountHead.BranchId);
                var OpeningAmount = new SqlParameter("@OpeningAmount", accountHead.OpeningAmount);
                var OpeningType = new SqlParameter("@OpeningType", accountHead.OpeningType);
                var Description = new SqlParameter("@Description", accountHead.Description);
                var Editable = new SqlParameter("@Editable", accountHead.Editable);
                var financialyearId = new SqlParameter("@financialyearId", accountHead.financialyearId);
                var UserId = new SqlParameter("@UserId", accountHead.UserId);
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //_dbContext.Entry(accountHead).State = EntityState.Modified;
            //Save();
        }
        public async Task<string> GetaccountBalance(string Type, int HeadId, int companyId, int Branchid, int FinancialYearId)
        {
            string amt = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.stpro_AccoundHead";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@AccountHeadid", SqlDbType.VarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@HeadName", SqlDbType.VarChar) { Value = Type });
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", SqlDbType.Int) { Value = HeadId });
                cmd.Parameters.Add(new SqlParameter("@AccountGroupId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@AccountSubGroupId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@AccountSubGroupName", SqlDbType.VarChar) { Value = "" });

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@OpeningAmount", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OpeningType", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Editable", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectBalance });



                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string balance = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    balance = balance + dataTable.Rows[i][0].ToString();
                }
                balance.Replace("{[", "").Replace("]}", "");
                return balance;

                //amt = dataTable.Rows[0][0].ToString();

            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                amt = "0";
            }
            return amt;

        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckAccountHeadEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }




        public async Task<string> Getledgermerge( int Branchid, int AccountTypeId)
        {
           
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.stpro_AccoundHead";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@AccountHeadid", SqlDbType.VarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@HeadName", SqlDbType.VarChar) { Value = ""});
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", SqlDbType.Int) { Value = AccountTypeId });
                cmd.Parameters.Add(new SqlParameter("@AccountGroupId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@AccountSubGroupId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@AccountSubGroupName", SqlDbType.VarChar) { Value = "" });

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@OpeningAmount", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OpeningType", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Editable", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value =0 });
                cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByTypeForLedgermerging });



                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<AccountHead>> GetByBranch(int Branchid)
        {
            try
            {
                var AccountHeadId = new SqlParameter("@AccountHeadId", "0");
                var AccountHeadName = new SqlParameter("@AccountHeadName", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountGroupId = new SqlParameter("@AccountGroupId", "0");
                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId ", "0");
                var AccountSubGroupName = new SqlParameter("@AccountSubGroupName", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var OpeningAmount = new SqlParameter("@OpeningAmount", "0");
                var OpeningType = new SqlParameter("@OpeningType", "0");
                var Description = new SqlParameter("@Description", "0");
                var Editable = new SqlParameter("@Editable", "0");
                var financialyearId = new SqlParameter("@financialyearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.ByBranch);
                var _product = await _dbContext.tbl_AccountHead.FromSqlRaw("stpro_AccoundHead @AccountHeadId, @AccountHeadName, @AccountTypeId, @AccountGroupId, @AccountSubGroupId, @AccountSubGroupName, @CompanyId, @BranchId, @OpeningAmount, @OpeningType, @Description, @Editable, @financialyearId,@UserId, @Action", AccountHeadId, AccountHeadName, AccountTypeId, AccountGroupId, AccountSubGroupId, AccountSubGroupName, CompanyId, BranchId, OpeningAmount, OpeningType, Description, Editable, financialyearId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
