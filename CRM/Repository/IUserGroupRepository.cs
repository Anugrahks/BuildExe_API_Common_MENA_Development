using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
    public interface IUserGroupRepository
    {
        Task<IEnumerable<UserGroup >> Get();
        Task<UserGroup> GetByID(int id);
        Task<IEnumerable<UserGroup>> Get(int companyid,int Branchid);
        Task<IEnumerable<Validation>> Insert(UserGroup userGroup);
        Task<IEnumerable<Validation>> Delete(int id);
        Task<IEnumerable<Validation>> Update(UserGroup userGroup);
        void Save();
    }
}
