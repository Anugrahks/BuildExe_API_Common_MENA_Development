using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
   public  interface IUserLogRepository
    {
        void Insert(int user, int Id, string formname, int action);
        void Save();
    }
}
