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
    public class AccountTypeRepository:IAccountTypeRepository 
    {
        private readonly BasicContext _dbContext;
        public AccountTypeRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task  <account_type_> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_AccountType.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<account_type_>> Get()
        {
            try
            {
                return await _dbContext.tbl_AccountType.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
