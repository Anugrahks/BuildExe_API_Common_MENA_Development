using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class AccountSubGroupRepository : IAccountSubGroupRepository
    {
        private readonly BasicContext _dbContext;
        public AccountSubGroupRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<AccountSubGroup>> GetByID(int accountSubGroupId)
        {
            try
            {
                return await _dbContext.tbl_AccountSubGroup.Where(x => x.AccountGroupId == accountSubGroupId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<AccountSubGroup>> GetSubGroup(int CompanyId, int Branchid, int AccountGroupId)
        {
            try
            {
                return await _dbContext.tbl_AccountSubGroup.Where(x => x.AccountGroupId == AccountGroupId).Where(x => x.CompanyId == CompanyId || x.CompanyId == 0).Where(x => x.BranchId == Branchid || x.BranchId == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AccountSubGroup>> Get()
        {
            try
            {
                return await _dbContext.tbl_AccountSubGroup.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
