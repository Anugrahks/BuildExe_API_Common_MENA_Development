using System;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public interface ISyncTablesRepository
    {
        Task Insert(string tableName, string jsonData, DateTime syncDate, int action);
        Task Update(string tableName, string jsonData, DateTime syncDate, int action);
    }
}
