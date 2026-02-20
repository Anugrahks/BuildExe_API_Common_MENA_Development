using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;


namespace BuildExeBasic.Repository
{ 
    public interface IProjectDetailExpenseRepository
    {
        Task<string> ProjectDetailExpenseReport(BasicSearch basicSearch);
        Task<IEnumerable<ProjectExpenseDetail>> ProjectExpenseReport(BasicSearch basicSearch);
    }
}
