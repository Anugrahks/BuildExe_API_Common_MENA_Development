using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class EmployeeLabourGroupRepository:IEmployeeLabourGroupRepository 
    {
        private readonly HRContext _dbContext;
        public EmployeeLabourGroupRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<EmployeeLabourGroup>> Get()
        {
            try
            {
                return await _dbContext.tbl_EmployeeLabourGroup.ToListAsync ();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
