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
    public class ReportFilterRepository:IReportFilterRepository 
    {
        private readonly BasicContext _dbContext;
        public ReportFilterRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            GetAll = 1,
            GetById = 2
        }
        public async Task<IEnumerable<ReportFilter >> GetByID(int meniid)
        {
            try
            {
                var menuId = new SqlParameter("@MenuId", meniid);
                var type = new SqlParameter("@Type", "Filters");
                var action = new SqlParameter("@Action", Actions.GetById);
                return await _dbContext.tbl_ReportFilter.FromSqlRaw("Stpro_GetReportFieldAndFilters @MenuId, @Type, @Action", menuId, type, action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ReportFilter>> Get()
        {
            try
            {
                var menuId = new SqlParameter("@MenuId", 0);
                var type = new SqlParameter("@Type", "Filters");
                var action = new SqlParameter("@Action", Actions.GetById);
                return await _dbContext.tbl_ReportFilter.FromSqlRaw("Stpro_GetReportFieldAndFilters @MenuId, @Type, @Action", menuId, type, action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
