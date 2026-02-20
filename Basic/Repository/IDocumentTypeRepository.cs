using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IDocumentTypeRepository
    {
        Task<string> Get(int CompanyId, int Branchid);
        Task<string> Get(int CompanyId, int Branchid, int UserId);
        Task<string>  GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(DocumentType documentType );
        Task<IEnumerable<Validation>> Delete(int Id,int userid);
        Task<IEnumerable<Validation>> Update(DocumentType documentType);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
