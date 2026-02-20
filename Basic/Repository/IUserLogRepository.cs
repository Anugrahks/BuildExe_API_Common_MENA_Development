using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IUserLogRepository
    {
        void Insert(int user, int Id, string formname, int action);
        void Save();
        Task<IEnumerable<ApprovalRemarks>> GetRemarks(int id, int menuid);
    }
}
