using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;

namespace BuildExeBasic.Repository
{
   public interface IDocumentGroupRepository
    {
        Task<string> Get(int CompanyId, int Branchid);
        Task<string> Get(int CompanyId, int Branchid, int UserId);
        Task <string> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(DocumentGroup documentGroup  );
        Task<IEnumerable<Validation>> Delete(int Id,int UserId);
        Task<IEnumerable<Validation>> Update(DocumentGroup documentGroup );
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
