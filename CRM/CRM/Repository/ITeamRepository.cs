using BuildExeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Validation>> InsertTeam(IEnumerable<Team> teams);
        Task<IEnumerable<Validation>> Update(IEnumerable<Team> teams);
        Task<IEnumerable<Team>> GetTeamuser(int CompanyId, int BranchId, int UserId);
        Task<IEnumerable<Team>> GetTeambyID(int Id);
        Task<IEnumerable<Team>> GetTeam(int CompanyId, int BranchId);
        Task<IEnumerable<Team>> GetTeam();
        Task<IEnumerable<Validation>> Delete(int id,int UserId);
        Task<string> GetUsers(int teamId);
        Task<IEnumerable<TeamsUsers>> GetMembers(int CompanyId, int BranchId);

        Task<string> GetUsersStage(int teamid, int EnquiryId, int OrderId, int Status);
        Task<string> GetUsersStageFor(int teamid, int EnquiryFor);

    }
}
