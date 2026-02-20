using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISalaryItemHeadRepository
    {
        Task<IEnumerable<SalaryItemHead >> Get(int companyid,int branchid);
        Task<IEnumerable<SalaryItemHead>> NotVarying(int companyid, int branchid);
        Task<IEnumerable<SalaryItemHead>> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(SalaryItemHead salaryItemHead);
        Task<IEnumerable<Validation>> Delete(int Id,int UserId);
        Task<IEnumerable<Validation>> Update(SalaryItemHead salaryItemHead);
        void Save();
        Task<IEnumerable<SalaryItemHead>> Getvaryinghead(int companyid, int branchid);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);

        Task<string> GetdetailsbyID(int Id);

        Task<IEnumerable<SalaryItemHead>> FacilityHeads(int companyid, int branchid);
    }
}
