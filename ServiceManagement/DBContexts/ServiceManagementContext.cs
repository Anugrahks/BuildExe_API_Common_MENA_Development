using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BuildExeServiceManagement.Models;
namespace BuildExeServiceManagement.DBContexts
{
    public class ServiceManagementContext : DbContext
    {
        public ServiceManagementContext(DbContextOptions<ServiceManagementContext> options) : base(options)
        {
        }
     
        public DbSet<Validation> tbl_validations { get; set; }
        public DbSet<Validation> tbl_validation { get; set; }
       

    }
    
}

