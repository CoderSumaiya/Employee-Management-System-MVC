using MVCSpa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCSpa.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base("AppDbContext")
        { }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<AcademicDetail> AcademicDetail { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
    }
}