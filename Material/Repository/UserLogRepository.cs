using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Repository
{
    public class UserLogRepository:IUserLogRepository 
    {
        private readonly MaterialContext _dbContext;
        public UserLogRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Insert(int user, int Id, string formname, int action)
        {
            UserLogs userLogs = new UserLogs();
            userLogs.MasterId = Id;
            userLogs.UserId = Convert.ToInt16(user);
            userLogs.FormName = formname;
            userLogs.EntryDate = DateTime.Now;
            userLogs.Action = Convert.ToInt32(action);
            _dbContext.Add(userLogs);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
