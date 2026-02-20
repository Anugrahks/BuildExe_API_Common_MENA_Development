using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IQCRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<QC> mat);
        Task<string> GetbyBranch(BillSearch projectWorkSetting);


    }
}

