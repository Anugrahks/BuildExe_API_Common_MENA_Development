using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
    public interface IEnquiryForRepository
    {
       Task <IEnumerable<EnquiryFor >> Get();
        Task<EnquiryFor> GetByID(int id);
        Task<IEnumerable<EnquiryFor>> Getenquiryfor(int CompanyId, int BranchId);
        Task<IEnumerable<EnquiryFor>> Getenquiryforuser(int CompanyId, int BranchId, int UserId);
        Task<IEnumerable<Validation>> Insert(EnquiryFor enquiryFor);
        Task<IEnumerable<Validation>> Delete(int id,int UserId);
        Task<IEnumerable<Validation>> Update(EnquiryFor enquiryFor);
        void Save();
        Task<string> IsExistEnquiry(int EnquiryForId);
    }
}
