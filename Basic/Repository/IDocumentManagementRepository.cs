using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
   public interface IDocumentManagementRepository
    {
        Task <string> Get(int CompanyId, int Branchid);
        Task<string> Get(int CompanyId, int Branchid, int UserId);
        Task <string> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(DocumentManagement documentManagement);
        Task<IEnumerable<Validation>> Delete(int Id,int userId);
        Task<IEnumerable<Validation>> Update(DocumentManagement DocumentManagement);
        void Save();
        Task<string> GetFileID(int id);

        Task<string> Get(string formName, int masterId);

        Task<string> GetForDashboard(int Branchid, int UserId);
    }
}
