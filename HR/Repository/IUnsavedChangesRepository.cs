using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IUnsavedChangesRepository
    {
        Task<string> UnsavedChanges(int id, int purpose);
        Task<string> UnsavedChangesAdd(int purpose, string dateworked, int projectid);
        Task<string> UnsavedChangesAddnew(int purpose, DateTime fromdate, DateTime todate, int branchid, int isGroup);
    }
}

