using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models; 
namespace BuildExeServices.Repository
{
    public interface IProjectBookingRepository
    {
        Task<IEnumerable<ProjectBooking>> Get();
        Task<IEnumerable<ProjectBooking>> Get(int companyId, int branchid);
        Task<IEnumerable<ProjectBooking>> Getuser(int companyId, int branchid, int UserId);
        Task<IEnumerable<ProjectBooking>> GetByID(int projectId);
        Task<IEnumerable<Validation>> Insert(ProjectBooking projectBooking );
        Task<IEnumerable<Validation>> Delete(int Id,int userId);
        Task<IEnumerable<Validation>> Update(ProjectBooking projectBooking );
        Task<IEnumerable<ProjectBooking>> GetForReport(int projectId);
        Task<IEnumerable<ProjectBooking>>  GetForReport(int companyId, int branchId, int id);
        Task<IEnumerable<ProjectBooking>> GetForReportWithId(int companyId, int branchId, int id, int reportId);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);

    }
}
