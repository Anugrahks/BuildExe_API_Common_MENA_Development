using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IFollowupRepository 
    {
       Task < IEnumerable<Followup >> GetFollowup();
        Task<string> GetFollowupForReport(FollowupSearch followupSearch );
        Task<IEnumerable<FollowUpList>> GetFollowupbyEnquiry(int EnquiryId, int userId);
        Task<string> GetFollowup(int CompanyId, int BranchId);
        Task<string> GetFollowupuser(int CompanyId, int BranchId,int UserId);
        Task<string> GetFollowupsearch(EnquirySearch enquirySearch);
        Task<Followup> GetFollowupByID(int Id);

        Task<IEnumerable<Validation>> InsertFollowup(Followup followup);

        Task<IEnumerable<Validation>> InsertFollowupBulk(BillSearch followup);
        Task<IEnumerable<Validation>> DeleteFollowup(int Id,int userid);
        Task<IEnumerable<Validation>> UpdateFollowup(Followup followup);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);

        Task<string> DeleteFollowupEnquiry(int EnquiryId, int FollowUpId);
        Task<string> GetEnquiryId(int id);
    }
}
