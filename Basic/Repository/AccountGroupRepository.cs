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
    public class AccountGroupRepository:IAccountGroupRepository 
    {
        private readonly BasicContext _dbContext;
        public AccountGroupRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<account_group_>> GetByID(int accounttypeid)
        {
            try
            {
                return await _dbContext.tbl_AccountGroup.Where(x => x.account_type_id == accounttypeid).ToListAsync ();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<account_group_>> Get()
        {
            try
            { 
            return await  _dbContext.tbl_AccountGroup.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
