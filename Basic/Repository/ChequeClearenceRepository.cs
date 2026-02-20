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
using Newtonsoft.Json;

namespace BuildExeBasic.Repository
{
    public class ChequeClearenceRepository : IChequeClearenceRepository
    {
        private readonly BasicContext _dbContext;
        public ChequeClearenceRepository(BasicContext dbContext)
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
            SelectByClearenceStatus = 7
        }
        public async Task<IEnumerable<Validation>> Update(ChequeClearence chequeClearence)
        {
            try
            {
                var id = new SqlParameter("@id", chequeClearence.Id);
                var bankId = new SqlParameter("@BankId", chequeClearence.BankId);
                var ClearenceStatus = new SqlParameter("@ClearenceStatus", chequeClearence.ClearenceStatus);
                var CompanyId = new SqlParameter("@CompanyId", chequeClearence.CompanyId);
                var BranchId = new SqlParameter("@BranchId", chequeClearence.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", chequeClearence.FinancialYearId);
                var UserId = new SqlParameter("@UserId", chequeClearence.UserId);
                var Narration = new SqlParameter("@Narration", chequeClearence.Narration);
                var BouncingCharge = new SqlParameter("@BouncingCharge", chequeClearence.BouncingCharge);
                var ChequeDate = new SqlParameter("@ChequeDate", chequeClearence.ChequeDate.ToString().Split(" ")[0]);
                var Action = new SqlParameter("@Action", Actions.Update);
                var validation = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge, @ChequeDate, @BankId,  @Action", id, ClearenceStatus, CompanyId, BranchId, FinancialYearId, UserId, Narration, BouncingCharge, ChequeDate, bankId, Action).ToListAsync();
                return validation;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int ID, int userId)
        {
            try
            {
                var id = new SqlParameter("@id", ID);
                var bankId = new SqlParameter("@BankId", "0");
                var ClearenceStatus = new SqlParameter("@ClearenceStatus", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var Narration = new SqlParameter("@Narration", "");
                var BouncingCharge = new SqlParameter("@BouncingCharge", "0");
                var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge,@ChequeDate, @BankId, @Action", id, ClearenceStatus, CompanyId, BranchId, FinancialYearId, UserId, Narration, BouncingCharge, ChequeDate, bankId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ChequeClearence>> GetByID(int iD)
        {
            try
            {
                var id = new SqlParameter("@id", iD);
                var bankId = new SqlParameter("@BankId", "0");
                var ClearenceStatus = new SqlParameter("@ClearenceStatus", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Narration = new SqlParameter("@Narration", "");
                var BouncingCharge = new SqlParameter("@BouncingCharge", "0");
                var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_PendingChequeMaster.FromSqlRaw("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge,@ChequeDate,@BankId, @Action", id, ClearenceStatus, CompanyId, BranchId, FinancialYearId, UserId, Narration, BouncingCharge, ChequeDate, bankId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ChequeClearence>> Get(int companyId, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var bankId = new SqlParameter("@BankId", "0");
                var ClearenceStatus = new SqlParameter("@ClearenceStatus", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Narration = new SqlParameter("@Narration", "");
                var BouncingCharge = new SqlParameter("@BouncingCharge", "0");
                var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_PendingChequeMaster.FromSqlRaw("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge,@ChequeDate, @BankId, @Action", id, ClearenceStatus, CompanyId, BranchId, FinancialYearId, UserId, Narration, BouncingCharge, ChequeDate, bankId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ChequeClearence>> Getuser(int companyId, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var bankId = new SqlParameter("@BankId", "0");
                var ClearenceStatus = new SqlParameter("@ClearenceStatus", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var Userid = new SqlParameter("@UserId", UserId);
                var Narration = new SqlParameter("@Narration", "");
                var BouncingCharge = new SqlParameter("@BouncingCharge", "0");
                var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_PendingChequeMaster.FromSqlRaw("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge,@ChequeDate, @BankId, @Action", id, ClearenceStatus, CompanyId, BranchId, financialYearId, Userid, Narration, BouncingCharge, ChequeDate, bankId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ChequeClearence>> GetByType(int companyId, int branchid, string type)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var bankId = new SqlParameter("@BankId", "0");
                var ClearenceStatus = new SqlParameter("@ClearenceStatus", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Type = new SqlParameter("@Narration", type);
                var BouncingCharge = new SqlParameter("@BouncingCharge", "0");
                var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.SelectByType);
                var _product = await _dbContext.tbl_PendingChequeMaster.FromSqlRaw("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge,@ChequeDate, @BankId, @Action", id, ClearenceStatus, CompanyId, BranchId, FinancialYearId, UserId, Type, BouncingCharge, ChequeDate, bankId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ChequeClearence>> GetByClearenceStatus(int companyId, int branchid, int ClearenceStatus, DateTime FromDate, DateTime ToDate, string ChequeType)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { FromDate, ToDate, ChequeType });
                var id = new SqlParameter("@id", "0");
                var bankId = new SqlParameter("@BankId", "0");
                var clearenceStatus = new SqlParameter("@ClearenceStatus", ClearenceStatus);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Type = new SqlParameter("@Narration", json);
                var BouncingCharge = new SqlParameter("@BouncingCharge", "0");
                var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.SelectByClearenceStatus);
                var _product = await _dbContext.tbl_PendingChequeMaster.FromSqlRaw("Stpro_ChequeClearence @id, @ClearenceStatus, @CompanyId, @BranchId, @FinancialYearId,@UserId, @Narration, @BouncingCharge,@ChequeDate, @BankId, @Action", id, clearenceStatus, CompanyId, BranchId, FinancialYearId, UserId, Type, BouncingCharge, ChequeDate, bankId, Action).ToListAsync();

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
