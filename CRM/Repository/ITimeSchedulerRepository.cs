using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface ITimeSchedulerRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task<IEnumerable<Validation>> Insert(IEnumerable<TimeSchedulerMaster> projectSpecificationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<TimeSchedulerMaster> projectSpecificationMaster);

        Task<IEnumerable<Validation>> Delete(int id, int userid);


        Task<string> GetProgressData(int ProjectId, int DivisionId, int ScheduleNumber);

        Task<string> GetProgressDataSubWork(TimeSchedulerProgress timeSchedulerProgress);

        Task<string> GetGridReschedule(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetDashboardData(int ProjectId, int DivisionId);
        Task<string> GetApproval(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetGrid(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetById(int Id);

        Task<string> GetRescheduleById(int Id);


        

        Task<string> GetSchedule(int ProjectId , int DivisionId);

        Task<string> GetWorkName(int BranchId);

        Task<string> GetData(int ProjectId, int DivisionId);
        Task<string> GetSubWork(int BranchId);

        Task<IEnumerable<Validation>> Reschedule(IEnumerable<TimeSchedulerMaster> specificationMasters);
        Task<string> GetPlanDashboard(int ProjectId, int DivisionId, int ScheduleNumber, int UnitId);


        Task<string> GetDashboardDataSubWork(int ProjectId, int DivisionId, int ScheduleNumber);
        Task<string> GetPlanDashboardSubWork(int ProjectId, int DivisionId, int ScheduleNumber);









    }
}
