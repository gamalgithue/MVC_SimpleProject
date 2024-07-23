using FirstPro.DAL.Entities;
using FirstPro.DAL.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.DAL.Database
{
   public  partial class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> ops):base(ops)
        {
            
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server = JOO-PC\\MSSQLSERVER2022;database = firstpro_task ;integrated security = true;TrustServerCertificate=true;");
        //}
    }

    public partial class ApplicationDbContext {
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<District> Districts { get; set; }

    }


}
