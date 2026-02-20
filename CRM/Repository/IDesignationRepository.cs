using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
    public interface IDesignationRepository
    {
        IEnumerable<EmployeeDesignation> GetDesignation();
        EmployeeDesignation GetDesignationByID(int DesignationId);
        void InsertDesignation(EmployeeDesignation designation);
        void DeleteDesignation(int DesignationId);
        void UpdateDesignation(EmployeeDesignation designation);
        void Save();
    }
}
