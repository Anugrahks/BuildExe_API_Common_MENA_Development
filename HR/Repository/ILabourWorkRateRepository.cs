using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ILabourWorkRateRepository
    {
        Task<IEnumerable<LabourWorkRate>> Get(int companyid, int branchid);
        Task<string> Getworkrate(int companyid, int branchid);
        Task<string> Getworkratebyuser(int companyid, int branchid, int UserId);
        Task<string> Get(int companyid, int branchid, int spectypeid);
        Task<LabourWorkRate> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(LabourWorkRate labourWorkRate);
        Task<IEnumerable<Validation>> Delete(int Id, int userid);
        Task<IEnumerable<Validation>> Update(LabourWorkRate labourWorkRate);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
