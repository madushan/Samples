using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Configuration;
using System.Data.Entity;

namespace Repository.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext():base("DefaultConnection")
        {
        }
        public DbSet <Employee> employees
        {
            get;
            set;
        }
    }
}