using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;

namespace BuildExeBasic.Repository
{
    public interface IReportFieldRepository
    {
        Task<IEnumerable<ReportField >> Get();
        Task<IEnumerable<ReportField>> GetByID(int MenuId);
        Task<string> GetPrintableByID(int MenuId);
    }
}
