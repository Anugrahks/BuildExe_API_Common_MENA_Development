using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
    public interface ISyncProjectRepository
    {
        Task<string> ProjectBlockFloorAssign(SyncProject syncProject);
        Task<string> ProjectBookingCancellation(SyncProject syncProject);
        Task<string> ProjectConsultancyDetails(SyncProject syncProject);
        Task<string> ProjectDivision(SyncProject syncProject);
        Task<string> ProjectMaster(SyncProject syncProject);
        Task<string> ProjectPaymentMode(SyncProject syncProject);
        Task<string> ProjectStagePlanning(SyncProject syncProject);
        Task<string> ProjectStagePlanningDetails(SyncProject syncProject);
        Task<string> ProjectStagePlanningUsers(SyncProject syncProject);
        Task<string> ProjectStageSettings(SyncProject syncProject);
        Task<string> ProjectWorkStage(SyncProject syncProject);
        Task<string> OwnProjectDetails(SyncProject syncProject);
        Task<string> Company(SyncProject syncProject);
        Task<string> Users(SyncProject syncProject);
        Task<string> StageActivityDetails(SyncProject syncProject);
        Task<string> WorkEnquiryStageSetting(SyncProject syncProject);
        Task<string> WorkEnquiryStageSettingDetails(SyncProject syncProject);
        Task<string> WorkEnquiryStageSettingUsers(SyncProject syncProject);

    }
}
