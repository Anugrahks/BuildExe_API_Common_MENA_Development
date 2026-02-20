using System;
using System.Data;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BuildExeServices.Repository
{
    public class SyncTablesRepository : ISyncTablesRepository
    {
        private readonly ProductContext _dbContext;

        public SyncTablesRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(string tableName, string jsonData, DateTime syncDate, int action) 
        {
            var currentDate = DateTime.Now; // Use current date for the @Date parameter

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_AllTablesInsertandUpdate @TableName, @Date, @SyncDate, @json, @Action",
                new SqlParameter("@TableName", tableName),
                new SqlParameter("@Date", currentDate),
                new SqlParameter("@SyncDate", syncDate), // SyncDate parameter
                new SqlParameter("@json", jsonData), // Ensure this is of type string
                new SqlParameter("@Action", action));
        }


        public async Task Update(string tableName, string jsonData, DateTime syncDate, int action)
        {
            // Use the syncDate provided instead of DateTime.Now
            var currentDate = syncDate;

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_AllTablesInsertandUpdate @TableName, @Date, @json, @Action",
                new SqlParameter("@TableName", tableName),
                new SqlParameter("@Date", currentDate),  // SyncDate is passed here
                new SqlParameter("@json", jsonData),
                new SqlParameter("@Action", action));
        }

    }
}
