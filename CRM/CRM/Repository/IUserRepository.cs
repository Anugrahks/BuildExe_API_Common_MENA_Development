using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public  interface IUserRepository
    {
       Task<IEnumerable<Users>> Getkey(int Key);
        Task<Users> GetByID(int id, int Key);
        Task<IEnumerable<Users>> GetBySiteType(int Companyid, int BranchId,int siteUser);
        Task<IEnumerable<Users>> GetUsers(int Companyid, int BranchId);
        Task<String> GetUser(int Companyid, int BranchId);
        Task<int> Getforlogin(int Companyid, int BranchId, string UserName, string Password);
        Task<int> Getforlogin(Users usersm);
        Task<IEnumerable<Users>> GetByUserID(string id, int Key);
        Task<IEnumerable<Validation>> Insert(Users users);
        Task<IEnumerable<Validation>> Delete(int id);
        Task<IEnumerable<Validation>> Update(Users users);
        void Save();
        Task<string> GetUserDetails(int userId);

        Task<UserPermissionResponse> GetUserFinAdmin(int companyId, int branchId, int userId);

        Task<string> GetforNewLogin(Users users, string srcApp);
        Task<string> GetStarted(int Action);

        Task InsertLoginLog(string username, string ipAddress, DateTime loginTime);
        Task<IEnumerable<Validation>> changePassword(UsersChangePassword users);
    }

}
