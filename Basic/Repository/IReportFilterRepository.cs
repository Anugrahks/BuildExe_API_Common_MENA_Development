using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;

namespace BuildExeBasic.Repository
{
   public interface IReportFilterRepository
    {
        Task<IEnumerable<ReportFilter >> Get();
        Task<IEnumerable<ReportFilter>> GetByID(int MenuId);
    }
}
