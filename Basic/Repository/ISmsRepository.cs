using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using BuildExeBasic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BuildExeBasic.Repository
{
    public interface ISmsRepository
    {
        Task<string> GetSms(int CompanyId, int BranchId);
        Task<IEnumerable<Validation>> Post(Smsmodel smsmodel);
        Task<IEnumerable<Validation>> Put(Smsmodel smsmodel);
        Task<IEnumerable<Validation>> PutStatus(Smsmodel smsmodel);
        Task<string> GetFetch(int CompanyId, int BranchId);
    }
}
