using BuildExeBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface IPrintableReportFilter
    {
        Task<IEnumerable<PrintableReportFilter>> GetPrintableReportFilter(int menuId);
    }
}