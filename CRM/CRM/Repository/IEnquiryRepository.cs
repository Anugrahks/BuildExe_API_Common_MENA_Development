using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IEnquiryRepository
    {
        Task<IEnumerable<EnquiryForMobile >> GetEnquiry();
        Task<IEnumerable<Enquiry>> GetEnquiry(int CompanyId, int BranchId);
       Task<IEnumerable<EnquiryList>> GetEnquirylist(int CompanyId, int BranchId);
        Task<string> GetEnquirybylist(int CompanyId, int BranchId);
        Task<string> GetEnquirybylistreport(int CompanyId, int BranchId);
       // Task<IEnumerable<EnquiryList>> GetEnquirylistuser(int CompanyId, int BranchId, int UserId);
        Task<string> GetEnquirySearch(EnquirySearch enquirySearch );
       Task<string> GetEnquiryReport(EnquiryReportSearch enquiryReportSearch);
       Task<EnquiryForMobile> GetEnquiryByID(int branchId);
        Task<IEnumerable<Validation>> InsertEnquiry(Enquiry enquiry);
        Task<IEnumerable<Validation>> DeleteEnquiry(int enquiryId, int userid);
        Task<IEnumerable<Validation>> UpdateEnquiry(Enquiry enquiry);
        Task<long> GetEnquiryIdValidation(int Id, string Enquiryid, int companyid, int branchid);
        Task ImportEnquiriesFromCsv(string csvFilePath);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int type);

        Task<string> GetEnquiryByProj(int CompanyId, int BranchId);
        Task<string> GetEnquirylistuser(int CompanyId, int BranchId, int UserId, int Page, int PageSize);
        Task<string> DeleteMessage(int enquiryId, int userid);
        Task<string> GetLocationData();

        Task<string> UnqProspectName(int BranchId);
    }

}
