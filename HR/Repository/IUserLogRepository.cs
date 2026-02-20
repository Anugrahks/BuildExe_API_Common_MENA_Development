using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;  
namespace BuildExeHR.Repository
{
    public interface IUserLogRepository
    {
        void Insert(int user, int Id, string formname, int action);
        void Save();
    }
}
