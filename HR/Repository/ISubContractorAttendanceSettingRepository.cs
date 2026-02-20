using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
  public interface ISubContractorAttendanceSettingRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorAttendanceSetting > subContractorAttendanceSettings );
        Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorAttendanceSetting> subContractorAttendanceSettings);
        Task<IEnumerable<SubContractorAttendanceSetting>> GetbyID(int Id);

        Task< IEnumerable<SubContractorAttendanceSetting>> Get(int companyid,int branchid);
        Task<IEnumerable<SubContractorAttendanceSetting>> Get(int ProjectId, int UnitId, int BlockId, int FloorId, int subconID);
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task< string> GetForEdit(int companyid, int branchid);

        Task<string> GetForEdituser(int companyid, int branchid, int UserId);
        Task<string> GetOneDetails(int id);
        Task<string> GetdetsilsforBill(int id);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
