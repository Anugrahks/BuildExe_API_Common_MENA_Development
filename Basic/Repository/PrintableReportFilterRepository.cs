using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class PrintableReportFilterRepository : IPrintableReportFilter
    {
        private readonly BasicContext _dbContext;
        public PrintableReportFilterRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PrintableReportFilter>> GetPrintableReportFilter(int menuId)
        {
            try
            {
                var printableReportFilter = await _dbContext.tbl_printableReportFilter.Where(x => x.MenuId == menuId).ToListAsync();
                return printableReportFilter;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
