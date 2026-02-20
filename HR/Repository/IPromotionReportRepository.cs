using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IPromotionReportRepository
    {
        Task<string> Promotion(HRSearch hRSearch);
    }
}