using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class UserLogRepository:IUserLogRepository 
    {
        private readonly BasicContext _dbContext;
        public UserLogRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Insert(int user,int Id,string formname ,int action)
        {
            UserLogs userLogs  = new UserLogs();
            userLogs.MasterId = Id;
            userLogs.UserId = Convert.ToInt16(user);
            userLogs.FormName = formname;
            userLogs.EntryDate  = DateTime.Now ;
            userLogs.Action = Convert.ToInt32(action);
            _dbContext.Add(userLogs);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<IEnumerable<ApprovalRemarks>> GetRemarks(int id, int menuid)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var json = new SqlParameter("@json", "");

                var MenuId = new SqlParameter("@MenuId", menuid);

                var Action = new SqlParameter("@Action",1);
                var _product = await _dbContext.tbl_Remarks.FromSqlRaw("stpro_Remarks @Id, @json, @MenuId, @Action", Id, json, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

    }
}
