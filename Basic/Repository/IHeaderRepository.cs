using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
  public   interface IHeaderRepository
    {
        Task Insert(IEnumerable<Header> header);
        Task Delete(int CompanyId, int BranchId, int Menuid);
        Task Update(IEnumerable<Header> header);
        Task<IEnumerable<Header>> Get(int CompanyId, int Branchid, int Menuid);

        //----------------------------------------------------------
        Task InsertContent(IEnumerable<Content> contents);
        Task DeleteContent(int CompanyId, int BranchId, int Menuid);
        Task UpdateContent(IEnumerable<Content> contents);
        Task<IEnumerable<Content>> GetContent(int companyid, int Branchid, int menuid);
        //------------------------------------------------------
        Task InsertFooter(IEnumerable<Footer> contents);
        Task DeleteFooter(int CompanyId, int BranchId, int Menuid);
        Task UpdateFooter(IEnumerable<Footer> contents);
        Task<IEnumerable<Footer>> GetFooter(int companyid, int Branchid, int menuid);

    }
}
