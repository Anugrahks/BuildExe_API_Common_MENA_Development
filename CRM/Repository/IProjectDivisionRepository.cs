using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
  public  interface IProjectDivisionRepository
    {
        Task<IEnumerable<ProjectDivision>>   Getproject(int CompanyId, int Branchid);
        Task<IEnumerable<ProjectDivision>> GetBlock(int ProjectId);
        Task<IEnumerable<ProjectDivision>> GetFloor(int ProjectId,int BlockId);
        Task<IEnumerable<ProjectDivision>> GetUnit(int ProjectId, int BlockId,int FloorId);
        Task<long> Gettype(int ProjectId);
        Task<IEnumerable<ProjectDivision>> GetUnitByProjBlockFloor(int projectId, int blockId, int floorId);
        Task<IEnumerable<ProjectDivision>> GetFloorByProjBlock(int projectId, int blockId);
        Task<IEnumerable<ProjectDivision>> GetBlockByProj(int projectId);
        Task<IEnumerable<ProjectDivision>> GetProj(int companyId, int branchid);
        Task<IEnumerable<ProjectDivision>> GetUnitByProj(int projectId);
    }
}
