using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class BankRepository:IBankRepository 
    {
        private readonly BasicContext _dbContext;
        public BankRepository(BasicContext dbContext)
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
            SelectwithFin = 7
        }
        public async Task<IEnumerable<Validation>> Delete(int ID,int userID)
        {
            try
            {
                var id = new SqlParameter("@id", ID);
                var BankId = new SqlParameter("@BankId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var BankName = new SqlParameter("@BankName", "0");
                var BranchName = new SqlParameter("@BranchName", "0");
                var IfsCode = new SqlParameter("@IfsCode  ", "0");
                var Micr_Code = new SqlParameter("@Micr_Code", "0");

                var CurrentBalance = new SqlParameter("@CurrentBalance", "0");
                var BalanceType = new SqlParameter("@BalanceType", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountNo = new SqlParameter("@AccountNo", "0");
                var City = new SqlParameter("@City", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var IsOD = new SqlParameter("@IsOD", "0");
                var MinimumBalance = new SqlParameter("@MinimumBalance", "0");
                var userId = new SqlParameter("@userId", userID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var ODLimit = new SqlParameter("@ODLimit", "0");
                var ODDate = new SqlParameter("@ODDate", DateTime.Now);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("stpro_Bank @id, @BankId, @CompanyId, @BranchId, @BankName, @BranchName, @IfsCode, @Micr_Code, @CurrentBalance, @BalanceType,@AccountTypeId, @AccountNo, @City, @FinancialYearId,@IsOD,@MinimumBalance,@userId ,@Action, @ODLimit,@ODDate", id, BankId, CompanyId, BranchId, BankName, BranchName, IfsCode, Micr_Code, CurrentBalance, BalanceType, AccountTypeId, AccountNo, City, FinancialYearId, IsOD, MinimumBalance, userId, Action, ODLimit,ODDate).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task <Bank>  GetByID(int iD)
        {
            try { 
            return await _dbContext.tbl_Banks.FindAsync(iD);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

            //var id = new SqlParameter("@id", iD);
            //var BankId = new SqlParameter("@BankId", "0");
            //var CompanyId = new SqlParameter("@CompanyId", "0");
            //var BranchId = new SqlParameter("@BranchId", "0");
            //var BankName = new SqlParameter("@BankName", "0");
            //var BranchName = new SqlParameter("@BranchName", "0");
            //var IfsCode = new SqlParameter("@IfsCode  ", "0");
            //var Micr_Code = new SqlParameter("@Micr_Code", "0");

            //var CurrentBalance = new SqlParameter("@CurrentBalance", "0");
            //var BalanceType = new SqlParameter("@BalanceType", "0");
            //var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
            //var AccountNo = new SqlParameter("@AccountNo", "0");
            //var City = new SqlParameter("@City", "0");
            //var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            //var IsOD = new SqlParameter("@IsOD", "0");
            //var MinimumBalance = new SqlParameter("@MinimumBalance", "0");
            //var userId = new SqlParameter("@userId", "0");
            //var Action = new SqlParameter("@Action", Actions.Select);
            //var _product = _dbContext.tbl_Banks.FromSqlRaw("stpro_Bank @id, @BankId, @CompanyId, @BranchId, @BankName, @BranchName, @IfsCode, @Micr_Code, @CurrentBalance, @BalanceType,@AccountTypeId, @AccountNo, @City, @FinancialYearId,@IsOD,@MinimumBalance,@userId, @Action", id, BankId, CompanyId, BranchId, BankName, BranchName, IfsCode, Micr_Code, CurrentBalance, BalanceType, AccountTypeId, AccountNo, City, FinancialYearId, IsOD, MinimumBalance, userId, Action);
            //return _product;

        }

        public async Task<IEnumerable<Bank>> Get(int companyId, int branchid)
        {
            //try
            //{
            //    if (Branchid == 0)

            //        return await _dbContext.tbl_Banks.Where(p => p.CompanyId == CompanyId).OrderByDescending(i=>i.Id).ToListAsync();
            //    else
            //        return await _dbContext.tbl_Banks.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).OrderByDescending(i => i.Id).ToListAsync();
            //}
            //catch (Exception)
            //{ throw; }

            try
            {
                var id = new SqlParameter("@id", "1");
                var BankId = new SqlParameter("@BankId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var BankName = new SqlParameter("@BankName", "0");
                var BranchName = new SqlParameter("@BranchName", "0");
                var IfsCode = new SqlParameter("@IfsCode  ", "0");
                var Micr_Code = new SqlParameter("@Micr_Code", "0");

                var CurrentBalance = new SqlParameter("@CurrentBalance", "0");
                var BalanceType = new SqlParameter("@BalanceType", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountNo = new SqlParameter("@AccountNo", "0");
                var City = new SqlParameter("@City", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var IsOD = new SqlParameter("@IsOD", "0");
                var MinimumBalance = new SqlParameter("@MinimumBalance", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var ODLimit = new SqlParameter("@ODLimit", "0");
                var ODDate = new SqlParameter("@ODDate", DateTime.Now);
                var _product = _dbContext.tbl_Banks.FromSqlRaw("stpro_Bank @id, @BankId, @CompanyId, @BranchId, @BankName, @BranchName, @IfsCode, @Micr_Code, @CurrentBalance, @BalanceType,@AccountTypeId, @AccountNo, @City, @FinancialYearId,@IsOD,@MinimumBalance,@userId, @Action, @ODLimit, @ODDate", id, BankId, CompanyId, BranchId, BankName, BranchName, IfsCode, Micr_Code, CurrentBalance, BalanceType, AccountTypeId, AccountNo, City, FinancialYearId, IsOD, MinimumBalance, userId, Action, ODLimit,ODDate);
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Bank>> getwithfinancial(int companyId, int branchid, int financialYearId)
        {

            try
            {
                var id = new SqlParameter("@id", "1");
                var BankId = new SqlParameter("@BankId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var BankName = new SqlParameter("@BankName", "0");
                var BranchName = new SqlParameter("@BranchName", "0");
                var IfsCode = new SqlParameter("@IfsCode  ", "0");
                var Micr_Code = new SqlParameter("@Micr_Code", "0");

                var CurrentBalance = new SqlParameter("@CurrentBalance", "0");
                var BalanceType = new SqlParameter("@BalanceType", "0");
                var AccountTypeId = new SqlParameter("@AccountTypeId", "0");
                var AccountNo = new SqlParameter("@AccountNo", "0");
                var City = new SqlParameter("@City", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", financialYearId);
                var IsOD = new SqlParameter("@IsOD", "0");
                var MinimumBalance = new SqlParameter("@MinimumBalance", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectwithFin);
                var ODLimit = new SqlParameter("@ODLimit", "0");
                var ODDate = new SqlParameter("@ODDate", DateTime.Now);
                var _product = _dbContext.tbl_Banks.FromSqlRaw("stpro_Bank @id, @BankId, @CompanyId, @BranchId, @BankName, @BranchName, @IfsCode, @Micr_Code, @CurrentBalance, @BalanceType,@AccountTypeId, @AccountNo, @City, @FinancialYearId,@IsOD,@MinimumBalance,@userId, @Action, @ODLimit, @ODDate", id, BankId, CompanyId, BranchId, BankName, BranchName, IfsCode, Micr_Code, CurrentBalance, BalanceType, AccountTypeId, AccountNo, City, FinancialYearId, IsOD, MinimumBalance, userId, Action, ODLimit, ODDate);
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(Bank bank)
        {
            try
            {
                var id = new SqlParameter("@id", "1");
                var BankId = new SqlParameter("@BankId", bank.BankId);
                var CompanyId = new SqlParameter("@CompanyId", bank.CompanyId);
                var BranchId = new SqlParameter("@BranchId", bank.BranchId);
                var BankName = new SqlParameter("@BankName", bank.BankName);
                var BranchName = new SqlParameter("@BranchName", bank.BranchName);
                var IfsCode = new SqlParameter("@IfsCode", bank.IfsCode);
                var Micr_Code = new SqlParameter("@Micr_Code", bank.Micr_Code);

                var CurrentBalance = new SqlParameter("@CurrentBalance", bank.CurrentBalance);
                var BalanceType = new SqlParameter("@BalanceType", bank.BalanceType);
                var AccountTypeId = new SqlParameter("@AccountTypeId", bank.AccountTypeId);
                var AccountNo = new SqlParameter("@AccountNo", bank.AccountNo);
                var City = new SqlParameter("@City", bank.City);
                var FinancialYearId = new SqlParameter("@FinancialYearId", bank.FinancialYearId);
                var IsOD = new SqlParameter("@IsOD", bank.IsOD);
                var MinimumBalance = new SqlParameter("@MinimumBalance", bank.MinimumBalance);
                var userId = new SqlParameter("@userId", bank.UserId);
                var Action = new SqlParameter("@Action", Actions.Insert);
                var ODLimit = new SqlParameter("@ODLimit", bank.ODLimit);
                var ODDate = new SqlParameter("@ODDate", bank.ODDate);
                
               

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_Bank @id, @BankId, @CompanyId, @BranchId, @BankName, @BranchName, @IfsCode, @Micr_Code, @CurrentBalance, @BalanceType , @AccountTypeId, @AccountNo, @City, @FinancialYearId,@IsOD,@MinimumBalance,@userId, @Action,@ODLimit,@ODDate", id, BankId, CompanyId, BranchId, BankName, BranchName, IfsCode, Micr_Code, CurrentBalance, BalanceType, AccountTypeId, AccountNo, City, FinancialYearId, IsOD, MinimumBalance, userId, Action, ODLimit,ODDate).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        
        public async Task<IEnumerable<Validation>> Update(Bank bank )
        {
            try
            {
                var id = new SqlParameter("@id", bank.Id);
                var BankId = new SqlParameter("@BankId", bank.BankId);
                var CompanyId = new SqlParameter("@CompanyId", bank.CompanyId);
                var BranchId = new SqlParameter("@BranchId", bank.BranchId);
                var BankName = new SqlParameter("@BankName", bank.BankName);
                var BranchName = new SqlParameter("@BranchName", bank.BranchName);
                var IfsCode = new SqlParameter("@IfsCode  ", bank.IfsCode);
                var Micr_Code = new SqlParameter("@Micr_Code", bank.Micr_Code);

                var CurrentBalance = new SqlParameter("@CurrentBalance", bank.CurrentBalance);
                var BalanceType = new SqlParameter("@BalanceType", bank.BalanceType);
                var AccountTypeId = new SqlParameter("@AccountTypeId", bank.AccountTypeId);
                var AccountNo = new SqlParameter("@AccountNo", bank.AccountNo);
                var City = new SqlParameter("@City", bank.City);
                var FinancialYearId = new SqlParameter("@FinancialYearId", bank.FinancialYearId);
                var IsOD = new SqlParameter("@IsOD", bank.IsOD);
                var MinimumBalance = new SqlParameter("@MinimumBalance", bank.MinimumBalance);
                var userId = new SqlParameter("@userId", bank.UserId);
                var Action = new SqlParameter("@Action", Actions.Update);
                var ODLimit = new SqlParameter("@ODLimit", bank.ODLimit);
                var ODDate = new SqlParameter("@ODDate", bank.ODDate);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_Bank @id, @BankId, @CompanyId, @BranchId, @BankName, @BranchName, @IfsCode, @Micr_Code, @CurrentBalance, @BalanceType,@AccountTypeId, @AccountNo, @City, @FinancialYearId,@IsOD,@MinimumBalance, @userId,@Action,@ODLimit,@ODDate", id, BankId, CompanyId, BranchId, BankName, BranchName, IfsCode, Micr_Code, CurrentBalance, BalanceType, AccountTypeId, AccountNo, City, FinancialYearId, IsOD, MinimumBalance, userId, Action, ODLimit,ODDate).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckBankAccountEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
