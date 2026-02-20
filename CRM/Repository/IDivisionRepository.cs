using BuildExeServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<Division>> Get();
        Task<string> Get(int companyid, int branchid);
        Task<Division> GetByID(int id);
        Task<IEnumerable<Division>> Getuser(int companyid, int branchid, int UserId);
        Task<IEnumerable<Validation>> Insert(Division enquiryStatus);
        Task<IEnumerable<Validation>> InsertDivision(DivisionProject divisionProject);
        Task<string> Delete(int divisionId, int userid);
        Task<IEnumerable<Validation>> Update(Division enquiryStatus);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int departmentId);
        Task<string> GetDefault(int companyid, int branchid);
        Task<string> GetProject(int ProjectId, int Branchid);

        Task<IEnumerable<Validation>> UpdateDivision(DivisionProject divisionProject);

        Task<string> GetProjectStatus(int ProjectId, int Branchid, int Status);
        Task<string> GetProjectall(int ProjectId, int Branchid);
    }
}
