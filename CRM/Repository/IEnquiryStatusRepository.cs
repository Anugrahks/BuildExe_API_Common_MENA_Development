using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IEnquiryStatusRepository
    {
        Task <IEnumerable<EnquiryStatus>> Get();
        Task<IEnumerable<EnquiryStatus>> Get(int companyid,int branchid);
        Task<EnquiryStatus> GetByID(int id);
        Task<IEnumerable<EnquiryStatus>> Getuser(int companyid, int branchid, int UserId);
        Task<IEnumerable<Validation>> Insert(EnquiryStatus enquiryStatus);
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task<IEnumerable<Validation>> Update(EnquiryStatus enquiryStatus);
        void Save();
    }
}
