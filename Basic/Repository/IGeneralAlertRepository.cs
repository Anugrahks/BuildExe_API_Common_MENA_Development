using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IGeneralAlertRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<GeneralAlert> generalAlert);
        Task<IEnumerable<Validation>> Update(IEnumerable<GeneralAlert> generalAlert);
        Task<IEnumerable<Validation>> Delete(int Id, int CompanyId, int BranchId);
        Task<IEnumerable<GeneralAlert>> Get(int CompanyId, int BranchId, int UserId);
        Task<IEnumerable<GeneralAlert>> GetById(int Id);
        Task<IEnumerable<Generalalertdoc>> Getdoc(int Id);


    }

}


