using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IEnquiryProfessionRepository
    {
        Task<string> GetByIdEnquiryProfessional(int Id);
        Task<IEnumerable<Validation>> PostEnquiryProfessional(IEnumerable<EnquiryProfession> enquiryProfession);
        Task<IEnumerable<Validation>> PutEnquiryProfessional(IEnumerable<EnquiryProfession> enquiryProfession);
        Task<string> GetEnquiryProfessional(int BranchId);
        Task<string> GetDeleteEnquiryProfessional(int Id);
    }
}
